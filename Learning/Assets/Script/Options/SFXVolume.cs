using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Utils;

public class SFXVolume : MonoBehaviour {
    public Slider sfx;
    public void OnPointClick() {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().efxSource.volume = sfx.value;
    }
}
