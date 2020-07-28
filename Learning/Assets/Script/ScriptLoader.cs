using UnityEngine;
using CSharpCompiler;
using System;

public class ScriptLoader : MonoBehaviour {

	private static ScriptLoader instance = null;
	DeferredSynchronizeInvoke synchronizedInvoke;
	ScriptBundleLoader loader;

	private ScriptLoader() { }
	public static ScriptLoader Instance {
		get {
			return instance;
		}
		private set {
			instance = value;
		}
	}

	public ScriptBundleLoader Loader {
		get {
			return loader;
		}

		private set {
			loader = value;
		}
	}

	void Awake() {
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}

	void Start() {
		synchronizedInvoke = new DeferredSynchronizeInvoke();

		loader = new CSharpCompiler.ScriptBundleLoader(synchronizedInvoke);
		loader.logWriter = new UnityLogTextWriter();
		loader.createInstance = (Type t) => {
			if (typeof(Component).IsAssignableFrom(t))
				return this.gameObject.AddComponent(t);
			else
				return System.Activator.CreateInstance(t);
		};
		loader.destroyInstance = (object instance) => {
			if (instance is Component)
				Destroy(instance as Component);
		};
	}


}
