using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine.UI;
using System.Reflection;
using System;

public class ItemUpgrade : MonoBehaviour {
	public Button button;
	public Equipamento equipamento;
	private GameObject instanciaInfoBoxDesc;
	public int valorUpgrade;

	void Start() {
		button.onClick.AddListener(() => Show());
	}
	
	void Show() {
		if (button.name == "melhorar") {
			if (GameObject.Find("Canvas/PainelInventario/Menu/InfoBoxList/InfoBoxDesc/InfoBoxDesc2") != null)
				Destroy(GameObject.Find("Canvas/PainelInventario/Menu/InfoBoxList/InfoBoxDesc/InfoBoxDesc2"));
		
			instanciaInfoBoxDesc = instanciaObjeto(186, 0, 170, 150, Resources.Load<GameObject>("InfoBoxDesc"), GameObject.Find("Canvas/PainelInventario/Menu/InfoBoxList/InfoBoxDesc").transform);

			instanciaObjetoLista(2, -25, 132, 0, Resources.Load<GameObject>("Descricao"), instanciaInfoBoxDesc.transform.GetChild(0).GetChild(0).GetChild(0));
	}
	}
	GameObject instanciaObjeto(int posX, int posY, int width, int height, GameObject prefab, Transform pai) {
		GameObject objeto;

		objeto = Instancia(posX, posY, width, height, prefab, pai);

		objeto.name = prefab.name + "2";
		objeto.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "Item Melhorado";
		return objeto;
	}

	void instanciaObjetoLista(int posX, int posY, int width, int height, GameObject prefab, Transform pai) {
		GameObject objeto;

        objeto = Instancia(posX, posY, width, height, prefab, pai);

		objeto.transform.GetComponent<Text>().text = setUpgradeDescricao();

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
		GameObject content = GameObject.Find("Canvas/PainelInventario/Menu/InfoBoxList/InfoBoxDesc/InfoBoxDesc2/InfoBoxScroll/Viewport/Content");
		GameObject descricao = content.transform.GetChild(1).gameObject;
		content.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(descricao.GetComponent<RectTransform>().sizeDelta.x, 35 + descricao.GetComponent<RectTransform>().sizeDelta.y);
		yield break;
	}

	string setUpgradeDescricao() {
		string descricao = "";

		descricao += "Os bônus são gerados de forma aleatória.\n";
		descricao += "\nBônus gerados:\n";
		descricao += setBonusAleatorios();

		return descricao;
	}

	string setBonusAleatorios() {
		string descricao = "";
		List<AttributeName> atributosAleatorios = setAtributosBonus();
		int valor;
		foreach(AttributeName a in atributosAleatorios) {
			valor = UnityEngine.Random.Range(a.min, a.max);
			descricao += a.name + " +" + valor + "\n";
			equipamento[a.nameOriginal] = ((int)equipamento[a.nameOriginal]) + valor;
        }
		setModificacoes();

        return descricao;
	}

	void setModificacoes() {
		Player p = SelectedPlayer.getInstance().getPlayer();
		List<Equipamento> equipamentos = new List<Equipamento>();
		foreach(Equipamento e in p.atributos.inventario.Equipamentos) {
			if (e.nome == equipamento.nome) {
				equipamento.level = equipamento.level + 1;
				equipamentos.Add(equipamento);
            } else {
				equipamentos.Add(e);
			}
		}

		SelectedPlayer.getInstance().getPlayer().atributos = new Atributos(p.atributos.Level, p.atributos.Experiencia, p.atributos.ByteCoin - valorUpgrade, p.atributos.tipoClasse, equipamentos);
		SaveLoadController.GetInstance().SavePlayer();
		SelectedPlayer.isUpdate = true;
	}

	List<AttributeName> setAtributosBonus() {
		List<AttributeName> atributosAleatorios = SelectedPlayer.getInstance().getPlayer().atributos.inventario.bonusAleatorio(equipamento.tipoEquipamento);
		List<AttributeName> bonus = new List<AttributeName>();
		for (int i = 0; i < 3; i++) {
			int ramdom = UnityEngine.Random.Range(0, atributosAleatorios.Count);
			bonus.Add(atributosAleatorios[ramdom]);
			atributosAleatorios.Remove(bonus[i]);
		}
		return bonus;
	}
}
