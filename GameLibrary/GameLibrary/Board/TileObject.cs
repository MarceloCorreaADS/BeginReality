using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Board
{
    [System.Serializable]
    public class TileObject
    {
        public GameObject tile;
        public bool walkable = true; 
    }
}
