using UnityEngine;
using UnityEngine.UI;

public class UnitBackground : MonoBehaviour {
    public float alpha = 0.5f;
    Image image;
    GameObject unitBackground;

    void Awake() {
        unitBackground = Instantiate(GameManager.instance.unitBackground, new Vector3(0, 0, 0f), Quaternion.identity) as GameObject;
        unitBackground.name = "UnitBackground";
        unitBackground.transform.position = gameObject.transform.position;
        unitBackground.transform.SetParent(gameObject.transform);
        unitBackground.transform.localScale = new Vector3(1, 1, 1);
    }

    void Start() {
        image = unitBackground.transform.FindChild("Image").GetComponent<Image>();
        Color color = image.color;
        if (gameObject.tag == "Ally") {
            color.r = 0.308f;
            color.g = 0.34f;
            color.b = 0.544f;
        } else {
            color.r = 0.66f;
            color.g = 0.096f;
            color.b = 0.096f;
        }
        color.a = alpha;
        image.color = color;
    }
}
