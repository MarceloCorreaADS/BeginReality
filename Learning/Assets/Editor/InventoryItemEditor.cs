using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections.Generic;
using Board;

public class InventoryItemEditor : EditorWindow {

	public TileObjectList tileObjectList;
	private int viewIndex = 1;
	GameObject defaultObject = null;
	int lotItens = 0;
	private Vector2 _scrollCharacterPos;
	private Vector2 _scrollTilesPos;

	[MenuItem("Window/Tile Object Editor %#e")]
	static void Init() {
		EditorWindow.GetWindow(typeof(InventoryItemEditor));
	}

	void OnEnable() {
		if (EditorPrefs.HasKey("ObjectPath")) {
			string objectPath = EditorPrefs.GetString("ObjectPath");
			tileObjectList = AssetDatabase.LoadAssetAtPath(objectPath, typeof(TileObjectList)) as TileObjectList;
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
			CreateNewItemList();
		}
		GUILayout.EndHorizontal();

		GUILayout.Space(20);

		if (tileObjectList != null && tileObjectList.tilesObject.Count > 0) {
			defaultObject = EditorGUILayout.ObjectField("Tile Object", defaultObject, typeof(GameObject), false) as GameObject;
			if (GUILayout.Button("Set Default Floor")) {
				tileObjectList.tilesObject.ForEach(tileObject => tileObject.tile = defaultObject);
			}
		}


		GUILayout.Space(20);
		if (tileObjectList != null) {
			tileObjectList.col = EditorGUILayout.IntField("Col", tileObjectList.col, GUILayout.ExpandWidth(false));
			GUILayout.Space(10);
			tileObjectList.row = EditorGUILayout.IntField("Row", tileObjectList.row, GUILayout.ExpandWidth(false));
			GUILayout.Space(10);
			tileObjectList.pecasByteCoin = EditorGUILayout.IntField("Byte Coin", tileObjectList.pecasByteCoin, GUILayout.ExpandWidth(false));
			GUILayout.Space(10);
			tileObjectList.qtyMinInimigos = EditorGUILayout.IntField("Min Qty inimigos", tileObjectList.qtyMinInimigos, GUILayout.ExpandWidth(false));
			if (tileObjectList.qtyMinInimigos < 0)
				tileObjectList.qtyMinInimigos = 0;
			GUILayout.Space(10);
			tileObjectList.qtyMaxInimigos = EditorGUILayout.IntField("Max Qty inimigos", tileObjectList.qtyMaxInimigos, GUILayout.ExpandWidth(false));
			if (tileObjectList.qtyMaxInimigos < 0)
				tileObjectList.qtyMaxInimigos = 0;
			if (tileObjectList.qtyMaxInimigos < tileObjectList.qtyMinInimigos)
				tileObjectList.qtyMaxInimigos = tileObjectList.qtyMinInimigos;
		}
		GUILayout.Space(20);
		if (tileObjectList != null) {
			if (GUILayout.Button("Add Character")) {
				tileObjectList.charactersPos.Add(new CharacterPos());
			}
			if (tileObjectList.charactersPos != null) {
				_scrollCharacterPos = EditorGUILayout.BeginScrollView(_scrollCharacterPos, GUILayout.Height(250));
				foreach (CharacterPos characterPos in tileObjectList.charactersPos) {
					GUILayout.Label("CHARACTER", GUILayout.ExpandWidth(false));
					if (GUILayout.Button("Remove", GUILayout.ExpandWidth(false))) {
						tileObjectList.charactersPos.Remove(characterPos);
						break;
					}
					GUILayout.BeginHorizontal();
					GUILayout.Space(10);
					characterPos.gameObject = EditorGUILayout.ObjectField("Character", characterPos.gameObject, typeof(GameObject), false) as GameObject;
					if(characterPos.gameObject != null) {
						characterPos.tag = characterPos.gameObject.tag;
					}
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					GUILayout.Space(10);
					characterPos.startPosition = EditorGUILayout.Vector2Field("Start Position", characterPos.startPosition, GUILayout.ExpandWidth(false));
					GUILayout.EndHorizontal();
					if (characterPos.tag == "Enemy") {
						GUILayout.Space(10);
						GUILayout.BeginHorizontal();
						GUILayout.Space(10);
						characterPos.pecasByteCoin = EditorGUILayout.IntField("Byte Coin", characterPos.pecasByteCoin, GUILayout.ExpandWidth(false));
						GUILayout.EndHorizontal();
						GUILayout.Space(10);
						GUILayout.Label("Level na Dificuldade Facil", GUILayout.ExpandWidth(false));
						GUILayout.BeginHorizontal();
						GUILayout.Space(10);
						EditorGUIUtility.labelWidth = 50;
						characterPos.nivelDificuldadeFacil.nivelMin = EditorGUILayout.IntField("Min", characterPos.nivelDificuldadeFacil.nivelMin, GUILayout.Width(100));
						GUILayout.Space(50);
						characterPos.nivelDificuldadeFacil.nivelMax = EditorGUILayout.IntField("Max", characterPos.nivelDificuldadeFacil.nivelMax, GUILayout.ExpandWidth(false));
						EditorGUIUtility.labelWidth = 100;
						GUILayout.EndHorizontal();
						GUILayout.Label("Level na Dificuldade Media", GUILayout.ExpandWidth(false));
						GUILayout.BeginHorizontal();
						GUILayout.Space(10);
						EditorGUIUtility.labelWidth = 50;
						characterPos.nivelDificuldadeMedio.nivelMin = EditorGUILayout.IntField("Min", characterPos.nivelDificuldadeMedio.nivelMin, GUILayout.Width(100));
						GUILayout.Space(50);
						characterPos.nivelDificuldadeMedio.nivelMax = EditorGUILayout.IntField("Max", characterPos.nivelDificuldadeMedio.nivelMax, GUILayout.ExpandWidth(false));
						EditorGUIUtility.labelWidth = 100;
						GUILayout.EndHorizontal();
						GUILayout.Label("Level na Dificuldade Dificil", GUILayout.ExpandWidth(false));
						GUILayout.BeginHorizontal();
						GUILayout.Space(10);
						EditorGUIUtility.labelWidth = 50;
						characterPos.nivelDificuldadeDificil.nivelMin = EditorGUILayout.IntField("Min", characterPos.nivelDificuldadeDificil.nivelMin, GUILayout.Width(100));
						GUILayout.Space(50);
						characterPos.nivelDificuldadeDificil.nivelMax = EditorGUILayout.IntField("Max", characterPos.nivelDificuldadeDificil.nivelMax, GUILayout.ExpandWidth(false));
						EditorGUIUtility.labelWidth = 100;
						GUILayout.EndHorizontal();
					}
					GUILayout.Space(40);

				}
				EditorGUILayout.EndScrollView();
			}
			GUILayout.Space(20);
			GUILayout.BeginHorizontal();

			GUILayout.Space(20);

			if (GUILayout.Button("Prev", GUILayout.ExpandWidth(false))) {
				if (viewIndex > 1)
					viewIndex--;
			}
			GUILayout.Space(5);
			if (GUILayout.Button("Next", GUILayout.ExpandWidth(false))) {
				if (viewIndex < tileObjectList.tilesObject.Count) {
					viewIndex++;
				}
			}

			GUILayout.Space(60);

			if (GUILayout.Button("Add Tile Object", GUILayout.ExpandWidth(false))) {
				AddItem();
			}
			if (GUILayout.Button("Delete Tile Object", GUILayout.ExpandWidth(false))) {
				DeleteItem(viewIndex - 1);
			}

			GUILayout.EndHorizontal();

			_scrollTilesPos = EditorGUILayout.BeginScrollView(_scrollTilesPos, GUILayout.Height(100));
			GUILayout.BeginHorizontal();
			lotItens = EditorGUILayout.IntField("Lot", lotItens, GUILayout.ExpandWidth(false));
			if (lotItens < 0)
				lotItens = 0;
			if (GUILayout.Button("Add Lot Tile Object", GUILayout.ExpandWidth(false))) {
				AddLotItem();
			}
			GUILayout.EndHorizontal();
			if (tileObjectList.tilesObject.Count > 0) {
				GUILayout.BeginHorizontal();
				viewIndex = Mathf.Clamp(EditorGUILayout.IntField("Current Item", viewIndex, GUILayout.ExpandWidth(false)), 1, tileObjectList.tilesObject.Count);
				//Mathf.Clamp (viewIndex, 1, inventoryItemList.itemList.Count);
				EditorGUILayout.LabelField("of   " + tileObjectList.tilesObject.Count.ToString() + "  tiles", "", GUILayout.ExpandWidth(false));
				GUILayout.EndHorizontal();

				tileObjectList.tilesObject[viewIndex - 1].tile = EditorGUILayout.ObjectField("Tile Object", tileObjectList.tilesObject[viewIndex - 1].tile, typeof(GameObject), false) as GameObject;
				tileObjectList.tilesObject[viewIndex - 1].walkable = (bool) EditorGUILayout.Toggle("Walkable", tileObjectList.tilesObject[viewIndex - 1].walkable, GUILayout.ExpandWidth(false));

				GUILayout.Space(10);

			} else {
				GUILayout.Label("This Map is Empty.");
			}

			EditorGUILayout.EndScrollView();
		}
		if (GUI.changed) {
			EditorUtility.SetDirty(tileObjectList);
		}
	}

	void CreateNewItemList() {
		// There is no overwrite protection here!
		// There is No "Are you sure you want to overwrite your existing object?" if it exists.
		// This should probably get a string from the user to create a new name and pass it ...
		viewIndex = 1;
		tileObjectList = CreateTileObjectList.Create();
		if (tileObjectList) {
			tileObjectList.tilesObject = new List<TileObject>();
			string relPath = AssetDatabase.GetAssetPath(tileObjectList);
			EditorPrefs.SetString("ObjectPath", relPath);
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

	void AddItem() {
		TileObject newItem = new TileObject();
		tileObjectList.tilesObject.Add(newItem);
		viewIndex = tileObjectList.tilesObject.Count;
	}

	void AddLotItem() {
		for (int i = 0; i < lotItens; i++) {
			TileObject newItem = new TileObject();
			tileObjectList.tilesObject.Add(newItem);
		}
		viewIndex = tileObjectList.tilesObject.Count;
	}

	void DeleteItem(int index) {
		tileObjectList.tilesObject.RemoveAt(index);
	}
}