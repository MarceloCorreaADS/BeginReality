using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using GameLibrary.Utils;

namespace Character {
	public class ShowAbility : MonoBehaviour {

		public Button button;
		public List<Habilidade> habilidades;
		[HideInInspector]
		private GameObject instanciaInfoBoxDesc;

		void Start() {
			button.onClick.AddListener(() => Show());
		}

		void Show() {

			if (GameObject.Find("Canvas/InfoAlly/InfoBoxList") != null)
				Destroy(GameObject.Find("Canvas/InfoAlly/InfoBoxList"));
			
			instanciaInfoBoxDesc = instanciaObjeto(-75, -86, 160, 150, Resources.Load<GameObject>("InfoBoxList"), GameObject.Find("Canvas/InfoAlly").transform);

			if (habilidades.Count > 0)
				listarHabilidades(habilidades);

		}
		void listarHabilidades(List<Habilidade> habilidades) {
			int count = 0;
			foreach (Habilidade h in habilidades) {
				count++;
				instanciaObjetoLista(h, 0, 0, 0, 0, Resources.Load<GameObject>("GenericButton"), instanciaInfoBoxDesc.transform.GetChild(0).GetChild(0).GetChild(0), count);
			}
		}
		void instanciaObjetoLista(Habilidade h, int posX, int posY, int width, int height, GameObject prefab, Transform pai, int count) {
			GameObject objeto;
			objeto = Instancia(posX, posY, width, height, prefab, pai);
			objeto.name = h.nome;
			objeto.AddComponent<ShowAbilityDesc>();
			objeto.GetComponent<ShowAbilityDesc>().habilidade = h;
			objeto.GetComponent<ShowAbilityDesc>().button = objeto.GetComponent<Button>();

			objeto.transform.GetChild(0).GetComponent<Text>().text = objeto.name;
			objeto.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(123, 24 + (25.25F * count));
		}
		GameObject instanciaObjeto(int posX, int posY, int width, int height, GameObject prefab, Transform pai) {
			GameObject objeto;
			objeto = Instancia(posX, posY, width, height, prefab, pai);
			objeto.name = prefab.name;
			if (prefab.name == "InfoBoxList") {
				objeto.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "Habilidades";
				StartCoroutine(Gambi());
			} else if (prefab.name == "Close") {
				objeto.AddComponent<Close>();
				objeto.GetComponent<Close>().button = objeto.GetComponent<Button>();
			}

			return objeto;
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
		private IEnumerator Gambi() {
			yield return null;

			if (GameObject.Find("Canvas/InfoAlly/InfoBoxList") != null)
				instanciaObjeto(0, 0, 20, 20, Resources.Load<GameObject>("Close"), GameObject.Find("Canvas/InfoAlly/InfoBoxList").transform);

			yield break;
		}
	}
}
