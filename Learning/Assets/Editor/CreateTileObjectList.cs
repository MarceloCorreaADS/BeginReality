using UnityEngine;
using System.Collections;
using UnityEditor;
using Board;

public class CreateTileObjectList : MonoBehaviour {

    [MenuItem("Assets/Create/Tile Object List")]
    public static TileObjectList Create()
    {
        TileObjectList asset = ScriptableObject.CreateInstance<TileObjectList>();

        AssetDatabase.CreateAsset(asset, "Assets/TileObjectList.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}
