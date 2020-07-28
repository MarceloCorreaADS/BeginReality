using UnityEngine;
using System.Collections.Generic;
using Board;

[CreateAssetMenu(fileName = "Data", menuName = "TileObject/List", order = 1)]
public class CreateScriptableObject : ScriptableObject
{
    public List<TileObject> tileObjectList;
}
