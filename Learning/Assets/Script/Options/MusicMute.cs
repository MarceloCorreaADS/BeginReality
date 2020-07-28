using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Utils;

public class MusicMute : MonoBehaviour {
    public Toggle music;
    public void OnPointClick() {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().musicSource.mute = music.isOn ? false : true;

        GameObject.Find("ConfigMusica").transform.GetChild(2).GetComponent<Slider>().interactable = music.isOn;

    }
}
