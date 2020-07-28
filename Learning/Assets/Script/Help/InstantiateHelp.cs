using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InstantiateHelp : MonoBehaviour {
	public Button button;
	private GameObject instanciaBox;

	// Use this for initialization
	void Start() {
		button.onClick.AddListener(() => Show());
	}

	public void Show() {
		if (GameObject.Find("Canvas/Help") != null)
			Destroy(GameObject.Find("Canvas/Help"));

		instanciaBox = instanciaObjeto(0, 0, 0, 0, Resources.Load<GameObject>("Help/Help"), GameObject.Find("Canvas").transform);

		Help help = new Help();

		int count = 0;
		foreach (HelpDescription h in help.helpDescriptions) {
			count++;
			instanciaObjetoLista(0, (count - 1) * -25, 123, 25, Resources.Load<GameObject>("GenericButton"), instanciaBox.transform.GetChild(0).GetChild(0).GetChild(0), h, count);
		}

	}
	GameObject instanciaObjeto(int posX, int posY, int width, int height, GameObject prefab, Transform pai) {
		GameObject objeto;
		objeto = Instancia(posX, posY, width, height, prefab, pai);

		objeto.name = prefab.name;
		return objeto;
	}
	void instanciaObjetoLista(int posX, int posY, int width, int height, GameObject prefab, Transform pai, HelpDescription helpDescription, int count) {
		GameObject objeto;
		objeto = Instancia(posX, posY, width, height, prefab, pai);

		objeto.AddComponent<ControllerHelp>();
		objeto.transform.GetComponent<ControllerHelp>().button = objeto.transform.GetComponent<Button>();
		objeto.transform.GetComponent<ControllerHelp>().helpDescription = helpDescription;

		objeto.name = helpDescription.title;
		objeto.transform.GetChild(0).GetComponent<Text>().text = objeto.name;

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
