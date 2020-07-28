using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Character;
using System.Collections;

public class CharacterLista : MonoBehaviour {
	public Button button;
	public Habilidade habilidade;
	public Equipamento equipamento;
	private GameObject info;
	private GameObject instanciaInfoBoxDesc;

	void Start() {
		button = gameObject.GetComponent<Button>();
		button.onClick.AddListener(() => Show());
	}

	void Show() {
		
		instanciaInfoBoxDesc = instanciaObjeto(160, 0, 185, 150, Resources.Load<GameObject>("InfoBoxDesc"), GameObject.Find("Canvas/PainelInventario/Menu/InfoBoxList").transform);

		if (GameObject.Find("Canvas/PainelInventario/Menu/InfoBoxList/InfoBoxDesc") != instanciaInfoBoxDesc)
			Destroy(GameObject.Find("Canvas/PainelInventario/Menu/InfoBoxList/InfoBoxDesc"));


		if (habilidade != null || equipamento != null)
			instanciaObjetoLista(2, -25, 132, 0, Resources.Load<GameObject>("Descricao"), instanciaInfoBoxDesc.transform.GetChild(0).GetChild(0).GetChild(0));

		if (equipamento != null && equipamento.level < 10)
			instanciaObjeto(0, -20, 140, 25, Resources.Load<GameObject>("ManutencaoEquipe/ButtonUpgrade"), instanciaInfoBoxDesc.transform.GetChild(0).GetChild(0).GetChild(0));
	}
	void Update() {

		if (SelectedPlayer.isUpdate) {

			Transform descricao = null;
			Transform botao = null;
			GameObject InfoBox = instanciaInfoBoxDesc;
			descricao = InfoBox.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1);
			botao = InfoBox.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(2);

			descricao.GetComponent<Text>().text = setEquipamento();
			botao.GetChild(0).GetComponent<Text>().text = "Melhorar-ByteCoin " + setByteCoin();
			botao.GetComponent<ItemUpgrade>().valorUpgrade = setByteCoin();
			if (equipamento.level == 10)
				Destroy(botao.gameObject);
			StartCoroutine(Gambi());

			SelectedPlayer.isUpdate = false;
		}
	}
	GameObject instanciaObjeto(int posX, int posY, int width, int height, GameObject prefab, Transform pai) {
		GameObject objeto;

		objeto = Instancia(posX, posY, width, height, prefab, pai);

		if (prefab.name == "ButtonUpgrade") {
			objeto.name = "melhorar";
			objeto.transform.GetChild(0).GetComponent<Text>().text = "Melhorar-ByteCoin " + setByteCoin();

			objeto.AddComponent<ItemUpgrade>();
			objeto.GetComponent<ItemUpgrade>().button = objeto.GetComponent<Button>();
			objeto.GetComponent<ItemUpgrade>().equipamento = equipamento;
			objeto.GetComponent<ItemUpgrade>().valorUpgrade = setByteCoin();

			if (SelectedPlayer.getInstance().getPlayer().atributos.ByteCoin < setByteCoin()) {
				objeto.GetComponent<Button>().enabled = false;
			} else {
				objeto.GetComponent<Button>().enabled = true;
			}
		} else {
			objeto.name = prefab.name;
			objeto.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = button.name;
		}


		return objeto;
	}
	void instanciaObjetoLista(int posX, int posY, int width, int height, GameObject prefab, Transform pai) {
		GameObject objeto;

		objeto = Instancia(posX, posY, width, height, prefab, pai);

		if (equipamento != null && habilidade == null) {
			objeto.transform.GetComponent<Text>().text = setEquipamento();
		} else if (habilidade != null && equipamento == null) {
			objeto.transform.GetComponent<Text>().text = setHabilidade();
		}

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

		GameObject content = GameObject.Find("Canvas/PainelInventario/Menu/InfoBoxList/InfoBoxDesc/InfoBoxScroll/Viewport/Content");
		GameObject descricao = content.transform.GetChild(1).gameObject;

		content.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(descricao.GetComponent<RectTransform>().sizeDelta.x, 60 + descricao.GetComponent<RectTransform>().sizeDelta.y);

		yield break;
	}
	private string setHabilidade() {
		string descricao = "";
		descricao += habilidade.descricao + "\n\n";
		descricao += "Dados: " + habilidade.qtyDados + "d" + habilidade.valorDado + "\n";

		if (habilidade.IsOfensiva) {
			descricao += "Dano Médio: " + habilidade.DanoMedio + "\n";
		} else {
			if (habilidade.tipoHabilidade.Equals(TipoHabilidades.HP)) {
				descricao += "Cura Médio: " + habilidade.CuraMedia + "\n";
			} else if (habilidade.tipoHabilidade.Equals(TipoHabilidades.SP)) {
				descricao += "Rejuvescer Médio: " + habilidade.RejuvenescerMedio + "\n";
			} else if (habilidade.tipoHabilidade.Equals(TipoHabilidades.HPSP)) {
				descricao += "Cura Médio: " + habilidade.CuraMedia + "\n";
				descricao += "Rejuvescer Médio: " + habilidade.RejuvenescerMedio + "\n";
			}
		}
		descricao += "Custo de Sp: " + habilidade.custoSp + "\n";
		descricao += "Distância minima: " + habilidade.minDistancia + "\n";
		descricao += "Distância máxima: " + habilidade.maxDistancia + "\n";
		descricao += "Custo de Pontos de Ação: " + habilidade.custoPontoAcao + "\n";

		return descricao;
	}

	private string setEquipamento() {
		string descricao = "";
		descricao += equipamento.descricao + "\n\n";

		List<AttributeName> atributos = SelectedPlayer.getInstance().getPlayer().atributos.inventario.Atributos(equipamento.tipoEquipamento);
		foreach (AttributeName a in atributos) {
			if (((int)equipamento[a.nameOriginal]) > 0 || a.nameOriginal == "level")
				descricao += a.name + equipamento[a.nameOriginal] + "\n";
		}

		descricao += "\nAo melhorar o item serão gerados bonus aleatórios para esse item";

		return descricao;
	}

	int setByteCoin() {
		return (int)(300 / (equipamento.level + 1)) + (300 * (equipamento.level + 1));
	}
}