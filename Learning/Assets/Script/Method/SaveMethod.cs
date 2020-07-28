using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class SaveMethod : MonoBehaviour {
	public Button button;
	public int index;

	void Start() {
		button = GetComponent<Button>();
		button.onClick.AddListener(() => Save());
	}

	void Save() {

		string nomeMetodo = getTextChild(1);
		string retornoMetodo = getTextChild(2);
		string atributosMetodo = getTextChild(3);
		string codigoMetodo = getTextChild(4);
		Method method = new Method(nomeMetodo, retornoMetodo, atributosMetodo, codigoMetodo);

		if (GameObject.Find("AlertBox(Clone)") == false) {
			if (nomeMetodo == "" || retornoMetodo == "" || codigoMetodo == "") {
				InstanciaAlert("Preencha todos os campos requisitados!");
			} else {
				List<Method> methods = SaveLoadController.GetInstance().Load(SaveTypes.METHOD) as List<Method>;
				if (methods == null) {
					methods = new List<Method>();
				}
				if (index != -1) {
					methods[index] = method;
					save(methods);
				} else {
					Method methodExists = methods.Find(m => m.name == method.name);
					if (methodExists == null) {
						methods.Add(method);
						save(methods);
					} else {
						InstanciaAlert("Já existe um método com esse nome! Exclua o método ja existente ou mude o nome do método que deseja salvar!");
					}
				}
			}
		}
	}
	void save(List<Method> methods) {
		try {
			SaveLoadController.GetInstance().Save(SaveTypes.METHOD, methods);
			InstanciaAlert("Método salvo com sucesso!");
		} catch (Exception) {
			InstanciaAlert("Não foi possivel salvar o método!");
		}
		GameObject.Find("Canvas/Container/InfoBoxList").GetComponent<LoadListMethod>().carrega();
		GameObject.Find("Canvas/Container/NewMethod").GetComponent<NewMethod>().Create();
	}
	void InstanciaAlert(string text) {
			Vector3 infoPosition = transform.position;
			GameObject alertBox = Instantiate(Resources.Load<GameObject>("AlertBox"), infoPosition, Quaternion.identity) as GameObject;
			alertBox.transform.SetParent(GameObject.Find("Canvas").transform, false);
			alertBox.transform.localScale = new Vector3(1, 1, 1);
			Alert alert = alertBox.GetComponent<Alert>();
		alert.Create(text, new Button[] { button }, false);
	}
	string getTextChild(int filho) {
		string text = "";
		GameObject pai = GameObject.Find("Canvas/Container/Form");

		if (pai != null)
			text = pai.transform.GetChild(filho).GetChild(2).GetComponent<Text>().text;

		return text;
	}
}