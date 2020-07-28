using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Utils;

public class SFXMute : MonoBehaviour {
    public Toggle sfx;
    public void OnPointClick() {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().efxSource.mute = sfx.isOn ? false : true;

        GameObject.Find("ConfigSFX").transform.GetChild(2).GetComponent<Slider>().interactable = sfx.isOn;
    }
}
