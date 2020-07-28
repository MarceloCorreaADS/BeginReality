using System.Collections.Generic;
using System.Reflection;
using System.CodeDom.Compiler;
using System.IO;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System;
using Utils;
using UnityEngine;

namespace CSharpCompiler {
	public class ScriptBundleLoader {
		public Func<Type, object> createInstance = (Type type) => { return Activator.CreateInstance(type); };
		public Action<object> destroyInstance = delegate { };

		public TextWriter logWriter = Console.Out;

		ISynchronizeInvoke synchronizedInvoke;
		List<ScriptBundle> allFilesBundle = new List<ScriptBundle>();
		string[] assemblyReferences = null;

		public ScriptBundleLoader(ISynchronizeInvoke synchronizedInvoke) {
			this.synchronizedInvoke = synchronizedInvoke;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fileSources"></param>
		/// <returns>true on success, false on failure</returns>
		//public ScriptBundle LoadAndWatchScriptsBundle(IEnumerable<string> fileSources) {
		//	var bundle = new ScriptBundle(this, fileSources);
		//	allFilesBundle.Add(bundle);
		//	return bundle;
		//}
		public ScriptBundle LoadAndWatchScriptsBundle(StringBuilder sourceCode) {
			if (assemblyReferences == null) {
				try {
					var domain = System.AppDomain.CurrentDomain;
					Assembly[] assemblys = domain.GetAssemblies();
					if (assemblys != null)
						assemblyReferences = assemblys.Select(a => a.Location).ToArray();
				} catch (Exception) {
					Debug.Log("deu ruim");
				}
			}
			var bundle = new ScriptBundle(this, sourceCode, assemblyReferences);
			allFilesBundle.Add(bundle);
			return bundle;
		}

		/// <summary>
		/// Manages a bundle of files which form one assembly, if one file changes entire assembly is recompiled.
		/// </summary>
		public class ScriptBundle {
			Assembly assembly;
			//IEnumerable<string> filePaths;
			StringBuilder code;
			List<FileSystemWatcher> fileSystemWatchers = new List<FileSystemWatcher>();
			List<object> instances = new List<object>();
			ScriptBundleLoader manager;

			string[] assemblyReferences;
			public ScriptBundle(ScriptBundleLoader manager, IEnumerable<string> filePaths, string[] assemblyReferences) {
				//this.filePaths = filePaths.Select(x => Path.GetFullPath(x));
				this.manager = manager;
				this.assemblyReferences = assemblyReferences;

				manager.logWriter.WriteLine("loading " + string.Join(", ", filePaths.ToArray()));
				CompileFiles();
				CreateFileWatchers();
				CreateNewInstances();
			}
			public ScriptBundle(ScriptBundleLoader manager, StringBuilder code, string[] assemblyReferences) {
				//this.filePaths = filePaths.Select(x => Path.GetFullPath(x));
				this.manager = manager;
				this.code = code;

				this.assemblyReferences = assemblyReferences;


				//manager.logWriter.WriteLine("loading " + string.Join(", ", filePaths.ToArray()));
				CompileFiles();
				CreateFileWatchers();
				CreateNewInstances();
			}

			void CompileFiles() {
				//filePaths = filePaths.Where(x => File.Exists(x)).ToArray();

				CompilerParameters cp = new CompilerParameters();
				//options.GenerateExecutable = false;
				//options.GenerateInMemory = true;
				cp.ReferencedAssemblies.AddRange(assemblyReferences);
				//				cp.ReferencedAssemblies.Add("system.dll");
				//				cp.ReferencedAssemblies.Add("system.xml.dll");
				//				cp.ReferencedAssemblies.Add("system.data.dll");
				//				cp.ReferencedAssemblies.Add("system.windows.forms.dll");
				//				cp.ReferencedAssemblies.Add("system.drawing.dll");
				//#if UNITY_EDITOR
				//				cp.ReferencedAssemblies.Add(@"dll\UnityEngine.dll");
				//				cp.ReferencedAssemblies.Add(@"Assets\Plugins\GameLibrary.dll");
				//				cp.ReferencedAssemblies.Add(@"dll\UnityEngine.UI.dll");
				//				Debug.Log("Unity Editor");
				//#endif
				//#if UNITY_STANDALONE
				//				cp.ReferencedAssemblies.Add(@"BeginReality_Data\Managed\UnityEngine.dll");
				//				cp.ReferencedAssemblies.Add(@"BeginReality_Data\Managed\GameLibrary.dll");
				//				cp.ReferencedAssemblies.Add(@"BeginReality_Data\Managed\UnityEngine.UI.dll");
				//				Debug.Log("STANDALONE");
				//#endif

				cp.CompilerOptions = "/t:library";
				cp.GenerateInMemory = true;
				cp.GenerateExecutable = false;

				var compiler = new CodeCompiler();
				//CompilerResults cr = compiler.CompileAssemblyFromFileBatch(cp, filePaths.ToArray());
				CompilerResults cr = compiler.CompileAssemblyFromSource(cp, code.ToString());

				string errors = "";
				if (cr.Errors.HasErrors) {
					foreach (CompilerError error in cr.Errors) {
						ConsoleReport.LogReport("Ocorreu um erro na linha " + (error.Line - 7).ToString() + " e coluna " + error.Column.ToString());
						errors = errors + error.ErrorText + " linha: " + (error.Line - 7).ToString() + "\n";
					}
					throw new Exception(errors);
				}

				//foreach (var err in result.Errors) {
				//	manager.logWriter.WriteLine(err);
				//}

				this.assembly = cr.CompiledAssembly;
				object o = assembly.CreateInstance("CSCodeEvaler");

				Type t = assembly.GetType("CSCodeEvaler");
				if (t != null) {
					MethodInfo mi = t.GetMethod("EvalCode");
					mi.Invoke(o, null);
				}
			}
			void CreateFileWatchers() {
				//foreach (var filePath in filePaths) {
				//	FileSystemWatcher watcher = new FileSystemWatcher();
				//	fileSystemWatchers.Add(watcher);
				//	watcher.Path = Path.GetDirectoryName(filePath);
				//	/* Watch for changes in LastAccess and LastWrite times, and 
				//                   the renaming of files or directories. */
				//	watcher.NotifyFilter = NotifyFilters.LastWrite
				//	   | NotifyFilters.FileName | NotifyFilters.DirectoryName;
				//	watcher.Filter = Path.GetFileName(filePath);

				//	// Add event handlers.
				//	watcher.Changed += new FileSystemEventHandler((object o, FileSystemEventArgs a) => { Reload(recreateWatchers: false); });
				//	//watcher.Created += new FileSystemEventHandler((object o, FileSystemEventArgs a) => { });
				//	watcher.Deleted += new FileSystemEventHandler((object o, FileSystemEventArgs a) => { Reload(recreateWatchers: false); });
				//	watcher.Renamed += new RenamedEventHandler((object o, RenamedEventArgs a) => {
				//		filePaths = filePaths.Select(x => {
				//			if (x == a.OldFullPath)
				//				return a.FullPath;
				//			else
				//				return x;
				//		});
				//		Reload(recreateWatchers: true);
				//	});
				//	watcher.SynchronizingObject = manager.synchronizedInvoke;
				//	// Begin watching.
				//	watcher.EnableRaisingEvents = true;
				//}
			}
			void StopFileWatchers() {
				foreach (var w in fileSystemWatchers) {
					w.EnableRaisingEvents = false;
					w.Dispose();
				}
				fileSystemWatchers.Clear();
			}
			void Reload(bool recreateWatchers = false) {
				//manager.logWriter.WriteLine("reloading " + string.Join(", ", filePaths.ToArray()));
				StopInstances();
				CompileFiles();
				CreateNewInstances();
				if (recreateWatchers) {
					StopFileWatchers();
					CreateFileWatchers();
				}
			}
			void CreateNewInstances() {
				if (assembly == null)
					return;
				foreach (var type in assembly.GetTypes()) {
					manager.synchronizedInvoke.Invoke((System.Action) (() => {
						instances.Add(manager.createInstance(type));
					}), null);
				}
			}
			void StopInstances() {
				foreach (var instance in instances) {
					manager.synchronizedInvoke.Invoke((System.Action) (() => {
						manager.destroyInstance(instance);
					}), null);
				}
				instances.Clear();
			}
		}


	}

}