using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ExcludeMethod : MonoBehaviour {
	public Button button;
	public int index;
	// Use this for initialization
	void Start() {
		button = GetComponent<Button>();
		button.onClick.AddListener(() => Exclude());
	}

	void Exclude() {
		if (GameObject.Find("AlertBox(Clone)") == false) {
			List<Method> methods = SaveLoadController.GetInstance().Load(SaveTypes.METHOD) as List<Method>;
			if (methods == null) {
				methods = new List<Method>();
			}
			if (index != -1) {
					methods.Remove(methods[index]);
					SaveLoadController.GetInstance().Save(SaveTypes.METHOD, methods);
					GameObject.Find("Canvas/Container/InfoBoxList").GetComponent<LoadListMethod>().carrega();
					excludeTextChild();
					InstanciaAlert("O Método foi excluido com sucesso!");
					index = -1;
			} else {
				InstanciaAlert("Nenhum método foi selecionado!");
			}
		}
	}
	void excludeTextChild() {
		string texto = "";
		GameObject pai = GameObject.Find("Canvas/Container/Form");

		if (pai != null) {
			for (int i = 1; i <= 4; i++)
				pai.transform.GetChild(i).GetComponent<InputField>().text = texto;
		}

	}
	void InstanciaAlert(string text) {
		Vector3 infoPosition = transform.position;
		GameObject alertBox = Instantiate(Resources.Load<GameObject>("AlertBox"), infoPosition, Quaternion.identity) as GameObject;
		alertBox.transform.SetParent(GameObject.Find("Canvas").transform, false);
		alertBox.transform.localScale = new Vector3(1, 1, 1);
		Alert alert = alertBox.GetComponent<Alert>();
		alert.Create(text, new Button[] { button }, false);
	}
}
