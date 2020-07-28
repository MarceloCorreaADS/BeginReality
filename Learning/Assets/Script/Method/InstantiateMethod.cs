using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InstantiateMethod : MonoBehaviour {
	public Button button;
	public Method method;
	public int index;
	private GameObject form;

	void Start () {
		button.onClick.AddListener(() => Show());
	}

	void Show () {
		form = GameObject.Find("Canvas/Container/Form");
		GameObject salvar = GameObject.Find("Canvas/Container/Form/Salvar");

		addText(1, method.name);
		addText(2, method.returnType);
		addText(3, method.attributes);
		addText(4, method.method);

		salvar.GetComponent<SaveMethod>().index = index;

	}
	void addText(int filho, string texto) {
		form.transform.GetChild(filho).GetComponent<InputField>().text = texto;
		Debug.Log(texto);
	}
}
