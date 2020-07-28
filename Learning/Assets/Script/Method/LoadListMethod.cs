using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LoadListMethod : MonoBehaviour {

	void Start () {
		carrega();
	}
	public void carrega() {
		GameObject content = GameObject.Find("Canvas/Container/InfoBoxList/InfoBoxScroll/Viewport/Content");
		GameObject boxList = GameObject.Find("Canvas/Container/InfoBoxList/InfoBoxScroll/Viewport/Content/BoxList");
		content.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 25);
		if (boxList != null)
			GameObject.Destroy(boxList);

		boxList = Instancia(0, 0, 0, 25, Resources.Load<GameObject>("Method/BoxList"), content.transform);


		List <Method> methods = SaveLoadController.GetInstance().Load(SaveTypes.METHOD) as List<Method>;
		if (methods != null) {
			int count = 0;
			foreach (Method m in methods) {
				content.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 75 + ((count) * 50));
				//boxList.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 75 + ((count) * 50));
				instanciaObjetoLista(0, 0, 0, 50, Resources.Load<GameObject>("Method/BotaoListaMethod"), boxList.transform, m, count);
				count++;
			}
		}
	}

	void instanciaObjetoLista(float posX, int posY, int width, int height, GameObject prefab, Transform pai, Method method, int index) {
		GameObject objeto;
		string nomeMethod;

		nomeMethod = method.returnType + " " + method.name;
		if (method.attributes != "")
			nomeMethod += " (" + method.attributes + ")";
		else
			nomeMethod += "()";

		objeto = Instancia(posX, posY, width, height, prefab, pai);
		objeto.transform.GetChild(0).GetComponent<Text>().text = nomeMethod;
		objeto.GetComponent<InstantiateMethod>().button = objeto.GetComponent<Button>();
		objeto.GetComponent<InstantiateMethod>().method = method;
		objeto.GetComponent<InstantiateMethod>().index = index;
		objeto.transform.GetChild(1).GetComponent<ExcludeMethod>().button = objeto.transform.GetChild(1).GetComponent<Button>();
		objeto.transform.GetChild(1).GetComponent<ExcludeMethod>().index = index;
        objeto.name = nomeMethod;
	}
	GameObject Instancia(float posX, int posY, int width, int height, GameObject prefab, Transform pai) {
		GameObject objeto;
		Vector3 infoPosition = transform.position;
		objeto = Instantiate(prefab, infoPosition, Quaternion.identity) as GameObject;
		objeto.transform.SetParent(pai);
		objeto.transform.localPosition = new Vector3(posX, posY, 1);
		objeto.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
		objeto.transform.localScale = new Vector3(1, 1, 1);

		objeto.name = prefab.name;

		return objeto;
	}
}
