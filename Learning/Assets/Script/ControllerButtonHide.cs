using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControllerButtonHide : MonoBehaviour {
	public Button button;
	private GameObject info;

	void Start () {
		button.onClick.AddListener(() => hide());
	}
	void hide() {
		if (gameObject.name == "InfoAllyButton") { 
			info = GameObject.Find("Canvas/InfoAlly/InfoBackgroundAlly");
			setActive();
		}else if (gameObject.name == "InfoEnemyButton") { 
			info = GameObject.Find("Canvas/InfoEnemy/InfoBackgroundEnemy");
			setActive();
		} 
	}
	void setActive() {
		if (info.activeInHierarchy == true) {
			button.transform.Rotate(new Vector3(0, 0, 180));
			info.SetActive(false);
		} else if (info.activeInHierarchy == false) {
			button.transform.Rotate(new Vector3(0, 0, -180));
			info.SetActive(true);
		}
	}
}
