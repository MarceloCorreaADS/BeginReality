using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Utils;

public class MusicVolume : MonoBehaviour {
    public Slider music;
    public void OnPointClick() {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().musicSource.volume = music.value;
    }
}