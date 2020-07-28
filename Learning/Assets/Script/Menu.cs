using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameLibrary.Utils;

public class Menu : MonoBehaviour {
	public Button button;

	void Start() {
		button.onClick.AddListener(() => Show());
	}
	void Show() {
		GameObject prefab = new GameObject();

		if (gameObject.scene.name == "Battle")
			prefab = Resources.Load<GameObject>("Menu/MenuBattle");
		else
			prefab = Resources.Load<GameObject>("Menu/Menu");

		if (GameObject.Find("Canvas/Menu") != null)
			Destroy(GameObject.Find("Canvas/Menu"));
		else
			instanciaObjeto(0, 0, 100, 50, prefab, GameObject.Find("Canvas"));
	}
	void instanciaObjeto(int posX, int posY, int width, int height, GameObject prefab, GameObject pai) {
		GameObject objeto;
		Vector3 infoPosition = transform.position;
		objeto = Instantiate(prefab, infoPosition, Quaternion.identity) as GameObject;
		objeto.transform.SetParent(pai.transform);
		objeto.transform.localPosition = new Vector3(posX, posY, 1);
		objeto.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
		objeto.transform.localScale = new Vector3(1, 1, 1);

		objeto.name = prefab.name;
	}
}