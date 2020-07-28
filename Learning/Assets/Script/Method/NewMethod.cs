using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NewMethod : MonoBehaviour {
	public Button button;

	void Start() {
		button.onClick.AddListener(() => Create());
	}

	public void Create() {
		excludeTextChild();
	}
	void excludeTextChild() {
		string texto = "";
		GameObject pai = GameObject.Find("Canvas/Container/Form");

		if (pai != null) {
			for (int i = 1; i <= 4; i++)
				pai.transform.GetChild(i).GetComponent<InputField>().text = texto;

			pai.transform.GetChild(5).GetComponent<SaveMethod>().index = -1;
		}
	}
}
