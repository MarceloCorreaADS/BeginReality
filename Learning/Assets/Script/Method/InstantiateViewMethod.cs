using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class InstantiateViewMethod : MonoBehaviour {
	public Button button;
	private GameObject instanciaBox;

	// Use this for initialization
	void Start() {
		button.onClick.AddListener(() => Show());
	}

	public void Show() {
		if (GameObject.Find("Canvas/MethodView") != null)
			Destroy(GameObject.Find("Canvas/MethodView"));

		instanciaBox = instanciaObjeto(0, 0, 0, 0, Resources.Load<GameObject>("Method/MethodView"), GameObject.Find("Canvas").transform);

		List<Method> methods = SaveLoadController.GetInstance().Load(SaveTypes.METHOD) as List<Method>;

		int count = 0;
		foreach (Method m in methods) {
			count++;
			instanciaObjetoLista(0, 0, 123, 25, Resources.Load<GameObject>("GenericButton"), instanciaBox.transform.GetChild(0).GetChild(0).GetChild(0), m, count);
		}

	}
	GameObject instanciaObjeto(int posX, int posY, int width, int height, GameObject prefab, Transform pai) {
		GameObject objeto;
		objeto = Instancia(posX, posY, width, height, prefab, pai);

		objeto.name = prefab.name;
		return objeto;
	}
	void instanciaObjetoLista(int posX, int posY, int width, int height, GameObject prefab, Transform pai, Method method, int count) {
		GameObject objeto;
		objeto = Instancia(posX, posY, width, height, prefab, pai);

		string methodName = method.returnType + " " + method.name + "(" + method.attributes + ")";

		objeto.AddComponent<ControllerViewMethod>();
		objeto.transform.GetComponent<ControllerViewMethod>().button = objeto.transform.GetComponent<Button>();
		objeto.transform.GetComponent<ControllerViewMethod>().method = method;

		objeto.name = methodName;
		objeto.transform.GetChild(0).GetComponent<Text>().text = methodName;

		objeto.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(123, 24 + (25.25F * count));
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
