using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Board;

enum DRAWOPTIONF { paint, erase };

public class FieldSelect : EditorWindow {
	private static bool isEnabled;
	private Vector2 _scrollPos;
	private static Vector2 gridSize = new Vector2(0.32f, 0.32f);
	private static bool isGrid;
	private static bool isDraw;
	private static bool addBoxCollider;
	private static bool isObjmode;
	private static DRAWOPTIONF selected;
	private static GameObject parentObj;
	private static int layerOrd;
	private static string tagName;
	private int index;
	private string[] options;
	private Sprite[] allSprites;
	private string[] files;
	private static Sprite activeSprite;
	public GUIStyle textureStyle;
	public GUIStyle textureStyleAct;
	public static GameObject selectedObject;
	public static GameObject changeObject;
	string folder = "";
	float scale = 0F;
	public static TileObjectList tileObjectList;

	[MenuItem("Tools/FieldSelect")]
	private static void TilemapEditor() {
		EditorWindow.GetWindow(typeof(FieldSelect));
	}

	void Awake() {

	}

	void Update() {

	}

	public void OnInspectorUpdate() {
		// This will only get called 10 times per second.
		Repaint();
	}

	void OnEnable() {
		isEnabled = true;
		Editor.CreateInstance(typeof(SceneViewEventHandler));
	}

	void OnDestroy() {
		isEnabled = false;
	}

	public class SceneViewEventHandler : Editor {
		static SceneViewEventHandler() {
			SceneView.onSceneGUIDelegate += OnSceneGUI;
		}

		static void OnSceneGUI(SceneView aView) {
			Event hotkey_e = Event.current;
			switch (hotkey_e.type) {
				case EventType.KeyDown:
					if (hotkey_e.shift) {
						switch (hotkey_e.keyCode) {
							case KeyCode.P:
								selected = DRAWOPTIONF.paint;
								break;
							case KeyCode.E:
								selected = DRAWOPTIONF.erase;
								break;
							case KeyCode.F:
								isDraw = !isDraw;
								break;
							case KeyCode.G:
								isGrid = !isGrid;
								break;
						}
					}
					break;
			}

			if (isEnabled && isDraw) {
				Event e = Event.current;
				HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
				Vector2 mousePos = Event.current.mousePosition;
				mousePos.y = SceneView.currentDrawingSceneView.camera.pixelHeight - mousePos.y;
				Vector3 mouseWorldPos = SceneView.currentDrawingSceneView.camera.ScreenPointToRay(mousePos).origin;
				mouseWorldPos.z = layerOrd;

				if ((e.type == EventType.MouseDrag || e.type == EventType.MouseDown) && e.button == 0 && selectedObject != null) {
					if (gridSize.x > 0.05f && gridSize.y > 0.05f) {
						mouseWorldPos.x = Mathf.Floor(mouseWorldPos.x / gridSize.x) * gridSize.x + gridSize.x / 2.0f;
						mouseWorldPos.y = Mathf.Ceil(mouseWorldPos.y / gridSize.y) * gridSize.y - gridSize.y / 2.0f;
					}
					//if (isObjmode)
					//    mouseWorldPos.z = mouseWorldPos.y + (activeSprite.bounds.size.y / -2.0f);
					GameObject[] allgo = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
					int brk = 0;
					if (selected == DRAWOPTIONF.paint) {
						for (int i = 0; i < allgo.Length; i++) {
							if (allgo[i].GetComponent<SpriteRenderer>() != null && allgo[i].GetComponent<SpriteRenderer>().bounds.Contains(mouseWorldPos)) {
								if (tileObjectList != null) {
									brk++;
									changeObject = allgo[i];
									SpriteRenderer oldSprite = changeObject.GetComponent<SpriteRenderer>();
									SpriteRenderer newSprite = selectedObject.GetComponent<SpriteRenderer>();
									oldSprite.sprite = newSprite.sprite;
									oldSprite.sharedMaterial = newSprite.sharedMaterial;
									oldSprite.sortingLayerID = newSprite.sortingLayerID;
									oldSprite.sortingOrder = newSprite.sortingOrder;
									changeObject.transform.localScale = new Vector3(selectedObject.transform.localScale.x * 10, selectedObject.transform.localScale.y * 10, selectedObject.transform.localScale.z);
									BlockPos blockPos = changeObject.GetComponent<BlockPos>();
									BlockPos selectedBlockPost = selectedObject.GetComponent<BlockPos>();
									string path = "";
									if (selectedBlockPost != null && selectedBlockPost.path.Length > 0) {
										path = selectedBlockPost.path + "/";
										char[] chars = path.ToCharArray();
										if (chars[0].Equals('/')) {
											path = "";
											for (int c = 1; c < chars.Length; c++) {
												path += chars[c];
											}
										}
									}
									
									GameObject go = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Sprites/" + path + "" + selectedObject.name + ".prefab", typeof(GameObject)) as GameObject;
									if (go != null) {
										tileObjectList.tilesObject[blockPos.pos].tile = go;
									} else {
										Debug.Log("NULL");
									}
									EditorUtility.SetDirty(tileObjectList);
								}
								break;
							}
						}
					} else if (selected == DRAWOPTIONF.erase) {
						//for (int i = 0; i < allgo.Length; i++)
						//{
						//    if (Mathf.Approximately(allgo[i].transform.position.x, mouseWorldPos.x) && Mathf.Approximately(allgo[i].transform.position.y, mouseWorldPos.y) && Mathf.Approximately(allgo[i].transform.position.z, mouseWorldPos.z))
						//        GameObject.DestroyImmediate(allgo[i]);
						//}
					}
				}


				if (e.type == EventType.MouseDown && e.button == 1) {
					Selection.activeGameObject = null;
					GameObject[] allgo = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
					int brk = 0;
					for (int i = 0; i < allgo.Length; i++) {
						if (allgo[i].GetComponent<SpriteRenderer>() != null && allgo[i].GetComponent<SpriteRenderer>().bounds.Contains(mouseWorldPos)) {
							brk++;
							selectedObject = allgo[i];
							Debug.Log("Selected Object: " + selectedObject.name);
							break;
						}
					}
				}
				//if (e.type == EventType.MouseDrag && e.button == 0 && activeGo != null)
				//{
				//    if (gridSize.x > 0.05f && gridSize.y > 0.05f)
				//    {
				//        mouseWorldPos.x = Mathf.Floor(mouseWorldPos.x / gridSize.x) * gridSize.x + gridSize.x / 2.0f;
				//        mouseWorldPos.y = Mathf.Ceil(mouseWorldPos.y / gridSize.y) * gridSize.y - gridSize.y / 2.0f;
				//    }
				//    activeGo.transform.position = mouseWorldPos;
				//}

			}
		}
	}

	[CustomEditor(typeof(GameObject))]
	public class SceneGUITest : Editor {
		[DrawGizmo(GizmoType.NotInSelectionHierarchy)]
		static void RenderCustomGizmo(Transform objectTransform, GizmoType gizmoType) {
			if (isEnabled && isGrid) {
				Gizmos.color = Color.white;
				Vector3 minGrid = SceneView.currentDrawingSceneView.camera.ScreenPointToRay(new Vector2(0f, 0f)).origin;
				Vector3 maxGrid = SceneView.currentDrawingSceneView.camera.ScreenPointToRay(new Vector2(SceneView.currentDrawingSceneView.camera.pixelWidth, SceneView.currentDrawingSceneView.camera.pixelHeight)).origin;
				for (float i = Mathf.Round(minGrid.x / gridSize.x) * gridSize.x; i < Mathf.Round(maxGrid.x / gridSize.x) * gridSize.x && gridSize.x > 0.05f; i += gridSize.x)
					Gizmos.DrawLine(new Vector3(i, minGrid.y, 0.0f), new Vector3(i, maxGrid.y, 0.0f));
				for (float j = Mathf.Round(minGrid.y / gridSize.y) * gridSize.y; j < Mathf.Round(maxGrid.y / gridSize.y) * gridSize.y && gridSize.y > 0.05f; j += gridSize.y)
					Gizmos.DrawLine(new Vector3(minGrid.x, j, 0.0f), new Vector3(maxGrid.x, j, 0.0f));
				SceneView.RepaintAll();
			}
		}

	}

	void OnGUI() {

		GUILayout.BeginHorizontal();
		GUILayout.Label("Tile Object Editor", EditorStyles.boldLabel);
		if (tileObjectList != null) {
			if (GUILayout.Button("Show Tile List")) {
				EditorUtility.FocusProjectWindow();
				Selection.activeObject = tileObjectList;
			}
		}
		if (GUILayout.Button("Open Tile List")) {
			OpenTileList();
		}
		if (GUILayout.Button("New Tile List")) {
			EditorUtility.FocusProjectWindow();
			Selection.activeObject = tileObjectList;
		}
		GUILayout.EndHorizontal();

		textureStyle = new GUIStyle(GUI.skin.button);
		textureStyle.margin = new RectOffset(2, 2, 2, 2);
		textureStyle.normal.background = null;
		textureStyleAct = new GUIStyle(textureStyle);
		textureStyleAct.margin = new RectOffset(0, 0, 0, 0);
		textureStyleAct.normal.background = textureStyle.active.background;

		if (!Directory.Exists(Application.dataPath + "/Tilemaps/")) {
			Directory.CreateDirectory(Application.dataPath + "/Tilemaps/");
			AssetDatabase.CreateFolder("Assets", "Tilemaps");
			AssetDatabase.Refresh();
			Debug.Log("Created Tilemaps Directory");
		}
		files = Directory.GetFiles(Application.dataPath + "/Tilemaps/", "*.png");
		options = new string[files.Length];

		EditorGUILayout.LabelField("Tile Map", GUILayout.Width(256));
		for (int i = 0; i < files.Length; i++) {
			options[i] = files[i].Replace(Application.dataPath + "/Tilemaps/", "");
		}
		index = EditorGUILayout.Popup(index, options, GUILayout.Width(256));
		GUILayout.BeginHorizontal();
		isGrid = EditorGUILayout.Toggle(isGrid, GUILayout.Width(16));
		gridSize = EditorGUILayout.Vector2Field("Grid Size (0.05 minimum)", gridSize, GUILayout.Width(236));
		GUILayout.EndHorizontal();

		EditorGUILayout.LabelField("Parent Object", GUILayout.Width(256));
		parentObj = (GameObject) EditorGUILayout.ObjectField(parentObj, typeof(GameObject), true, GUILayout.Width(256));

		EditorGUILayout.LabelField("Layer Order", GUILayout.Width(256));

		GUILayout.BeginHorizontal();
		layerOrd = EditorGUILayout.IntField(layerOrd, GUILayout.Width(126));
		isObjmode = EditorGUILayout.Toggle(isObjmode, GUILayout.Width(16));
		EditorGUILayout.LabelField("Layer based on Y", GUILayout.Width(110));
		GUILayout.EndHorizontal();

		EditorGUILayout.LabelField("Tag", GUILayout.Width(32));
		GUILayout.BeginHorizontal();
		tagName = EditorGUILayout.TagField(tagName, GUILayout.Width(236));
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		isDraw = EditorGUILayout.Toggle(isDraw, GUILayout.Width(16));
		selected = (DRAWOPTIONF) EditorGUILayout.EnumPopup(selected, GUILayout.Width(236));
		GUILayout.EndHorizontal();

		_scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
		float ctr = 0.0f;
		if (options.Length > index) {
			allSprites = AssetDatabase.LoadAllAssetsAtPath("Assets/Tilemaps/" + options[index]).Select(x => x as Sprite).Where(x => x != null).ToArray();
			GUILayout.BeginHorizontal();
			GUILayout.Label("Folder", GUILayout.Width(50));
			folder = EditorGUILayout.TextField(folder);
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			GUILayout.Label("Scale", GUILayout.Width(50));
			scale = EditorGUILayout.FloatField(scale, GUILayout.Width(50));
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Generate Tiles")) {
				GenerateTiles(allSprites, folder, scale);
			};
			GUILayout.EndHorizontal();
			GUILayout.Space(20);
			GUILayout.BeginHorizontal();
			foreach (Sprite singsprite in allSprites) {
				if (ctr > singsprite.textureRect.x) {
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
				}
				ctr = singsprite.textureRect.x;
				if (activeSprite == singsprite) {
					GUILayout.Button("", textureStyleAct, GUILayout.Width(32 + 6), GUILayout.Height(32 + 4));
					GUI.DrawTextureWithTexCoords(new Rect(GUILayoutUtility.GetLastRect().x + 3f,
														  GUILayoutUtility.GetLastRect().y + 2f,
														  GUILayoutUtility.GetLastRect().width - 6f,
														  GUILayoutUtility.GetLastRect().height - 4f),
												 singsprite.texture,
												 new Rect(singsprite.textureRect.x / (float) singsprite.texture.width,
							 singsprite.textureRect.y / (float) singsprite.texture.height,
							 singsprite.textureRect.width / (float) singsprite.texture.width,
							 singsprite.textureRect.height / (float) singsprite.texture.height));
				} else {
					if (GUILayout.Button("", textureStyle, GUILayout.Width(32 + 2), GUILayout.Height(32 + 2)))
						activeSprite = singsprite;
					GUI.DrawTextureWithTexCoords(GUILayoutUtility.GetLastRect(), singsprite.texture,
												 new Rect(singsprite.textureRect.x / (float) singsprite.texture.width,
															 singsprite.textureRect.y / (float) singsprite.texture.height,
															 singsprite.textureRect.width / (float) singsprite.texture.width,
															 singsprite.textureRect.height / (float) singsprite.texture.height));
				}
			}
			GUILayout.EndHorizontal();
		}
		EditorGUILayout.EndScrollView();
		SceneView.RepaintAll();
	}

	void GenerateTiles(Sprite[] sprites, string folder, float scale) {
		string folder2 = "";
		if (folder.Length > 0) {
			if (!Directory.Exists(Application.dataPath + "/Prefabs/Sprites/" + folder + "/")) {
				Directory.CreateDirectory(Application.dataPath + "/Prefabs/Sprites/" + folder + "/");
				AssetDatabase.Refresh();
				Debug.Log("Created Sprite Folder Directory");
			}
			folder2 = folder + "/";
		}

		foreach (Sprite sprite in allSprites) {
			GameObject basePrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Sprites/Base.prefab", typeof(GameObject)) as GameObject;
			SpriteRenderer spriteRenderer = basePrefab.GetComponent<SpriteRenderer>();
			spriteRenderer.sprite = sprite;
			basePrefab.GetComponent<BlockPos>().path = folder;
			basePrefab.GetComponent<BlockPos>().scaleMultiplier = scale;
			basePrefab.transform.localScale = new Vector3(scale, scale, 1);
			PrefabUtility.CreatePrefab("Assets/Prefabs/Sprites/" + folder2 + "" + sprite.name + ".prefab", basePrefab);
		}
	}

	void OpenTileList() {
		string absPath = EditorUtility.OpenFilePanel("Select Tile Object List", "", "");
		if (absPath.StartsWith(Application.dataPath)) {
			string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
			tileObjectList = AssetDatabase.LoadAssetAtPath(relPath, typeof(TileObjectList)) as TileObjectList;
			if (tileObjectList.tilesObject == null)
				tileObjectList.tilesObject = new List<TileObject>();
			if (tileObjectList) {
				EditorPrefs.SetString("ObjectPath", relPath);
			}
		}
	}
}
