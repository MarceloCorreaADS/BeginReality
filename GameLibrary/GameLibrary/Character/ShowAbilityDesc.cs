using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Character {
	public class ShowAbilityDesc : MonoBehaviour {
		public Habilidade habilidade;
		public Button button;
		private GameObject info;
		private GameObject instanciaInfoBoxDesc;

		void Start() {
			button.onClick.AddListener(() => Show());
		}
		void Show() {
			if (GameObject.Find("Canvas/InfoAlly/InfoBoxDesc/InfoBoxDesc") != null)
				Destroy(GameObject.Find("Canvas/InfoAlly/InfoBoxDesc/InfoBoxDesc"));

			instanciaInfoBoxDesc = instanciaObjeto(160, 0, 185, 150, Resources.Load<GameObject>("InfoBoxDesc"), GameObject.Find("Canvas/InfoAlly/InfoBoxList").transform);

			if (habilidade != null)
				instanciaObjetoLista(2, -25, 147, 0, Resources.Load<GameObject>("Descricao"), instanciaInfoBoxDesc.transform.GetChild(0).GetChild(0).GetChild(0));
		}
		GameObject instanciaObjeto(int posX, int posY, int width, int height, GameObject prefab, Transform pai) {
			GameObject objeto;

			objeto = Instancia(posX, posY, width, height, prefab, pai);

			objeto.name = prefab.name;
			objeto.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = button.name;

			return objeto;
		}
		void instanciaObjetoLista(int posX, int posY, int width, int height, GameObject prefab, Transform pai) {
			GameObject objeto;

			objeto = Instancia(posX, posY, width, height, prefab, pai);

			objeto.transform.GetComponent<Text>().text = setHabilidade();
		
			objeto.name = prefab.name;

			StartCoroutine(Gambi());
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

			GameObject content = GameObject.Find("Canvas/InfoAlly/InfoBoxList/InfoBoxDesc/InfoBoxScroll/Viewport/Content");
			GameObject descricao = content.transform.GetChild(1).gameObject;

			content.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(descricao.GetComponent<RectTransform>().sizeDelta.x, 25 + descricao.GetComponent<RectTransform>().sizeDelta.y);

			yield break;
		}
		private string setHabilidade() {
			string descricao = "";
			descricao += habilidade.descricao + "\n\n";
			descricao += "Dados: " + habilidade.qtyDados + "d" + habilidade.valorDado + "\n";

			if (habilidade.IsOfensiva)
				descricao += "Dano Médio: " + habilidade.DanoMedio + "\n";
			else
				descricao += "Cura Médio: " + habilidade.CuraMedia + "\n";

			descricao += "Custo de Sp: " + habilidade.custoSp + "\n";
			descricao += "Distância minima: " + habilidade.minDistancia + "\n";
			descricao += "Distância máxima: " + habilidade.maxDistancia + "\n";
			descricao += "Custo de Pontos de Ação: " + habilidade.custoPontoAcao + "\n";

			return descricao;
		}
	}
}
