using UnityEngine;
using UnityEngine.UI;

public class Alert : MonoBehaviour {

	public Text txtAlert;
	public Button btnAlert;
	
	public void Create(string text, Button[] btns, bool removeButton) {
		txtAlert.text = text;
		if (removeButton) {
			Destroy(btnAlert.gameObject);
		} else {
			btnAlert.onClick.AddListener(() => DestroyAlert(btns));
		}
	}

	public void DestroyAlert(Button[] btns) {
		foreach(Button btn in btns) {
			btn.interactable = true;
		}
		Destroy(gameObject);
	}

	public static GameObject InstantiateAlert() {
		Transform canvasTransform = GameObject.Find("Canvas").transform;
        GameObject alertBox = Instantiate(Resources.Load<GameObject>("AlertBox"));
		alertBox.transform.SetParent(canvasTransform, false);
		alertBox.transform.position = canvasTransform.position;
		return alertBox;
	}
}
