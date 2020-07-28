using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Utils;

public class OptionClick : MonoBehaviour {
    public Button button;
    private GameObject instanciaBox;

    // Use this for initialization
    void Start() {
        button.onClick.AddListener(() => Show());
    }
    public void Show() {
        if (GameObject.Find("Canvas/Option ") != null)
            Destroy(GameObject.Find("Canvas/Option"));

        instanciaBox = instanciaObjeto(0, 0, 0, 0, Resources.Load<GameObject>("Option/Option"), GameObject.Find("Canvas").transform);

        GameObject configMusica = GameObject.Find("ConfigMusica");
        GameObject configSFX = GameObject.Find("ConfigSFX");
        SoundManager soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        configMusica.transform.GetChild(0).GetComponent<Toggle>().isOn = soundManager.musicSource.mute ? false : true;
        configMusica.transform.GetChild(2).GetComponent<Slider>().value = soundManager.musicSource.volume;

        configSFX.transform.GetChild(0).GetComponent<Toggle>().isOn = soundManager.efxSource.mute ? false : true;
        configSFX.transform.GetChild(2).GetComponent<Slider>().value = soundManager.efxSource.volume;
    }
    GameObject instanciaObjeto(int posX, int posY, int width, int height, GameObject prefab, Transform pai) {
        GameObject objeto;
        objeto = Instancia(posX, posY, width, height, prefab, pai);

        objeto.name = prefab.name;
        return objeto;
    }

    GameObject Instancia(int posX, int posY, int width, int height, GameObject prefab, Transform pai) {
        GameObject objeto;
        Vector3 infoPosition = transform.position;
        objeto = Instantiate(prefab, infoPosition, Quaternion.identity) as GameObject;
        objeto.transform.SetParent(pai);
        objeto.transform.localPosition = new Vector3(posX, posY, 1);
        objeto.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        objeto.transform.localScale = new Vector3(1, 1, 1);

        return objeto;
    }
}