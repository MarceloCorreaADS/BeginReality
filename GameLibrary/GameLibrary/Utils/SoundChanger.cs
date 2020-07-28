using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Utils;

namespace GameLibrary.Utils {
    public class SoundChanger : MonoBehaviour {
        void Start() {
            GameObject.Find("SoundManager").GetComponent<SoundManager>().ChangeSound(gameObject.scene.name);
        }
    }
}
