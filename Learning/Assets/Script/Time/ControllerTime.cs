using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Character;
using Utils;

public class ControllerTime : MonoBehaviour {
	public Button button;
	private GameObject info;
	private Player player = null;

	void Start() {
		button.onClick.AddListener(() => Show());
	}
	void Update() {
        if(SelectedPlayer.isUpdate && info != null) {
			info.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = setAtributos(player);
		}
	}
	void Show() {
		Transform infoBox = null;
		player = GetComponent<Player>();
		SelectedPlayer.getInstance().setPlayer(player);
		if (GameObject.Find("Canvas/PainelInventario") != null)
			Destroy(GameObject.Find("Canvas/PainelInventario"));

		GameObject instancia = instanciaObjeto(0, -35, 0, 0, Resources.Load<GameObject>("ManutencaoEquipe/PainelInventario"), "Canvas");
		info = instancia.transform.GetChild(0).gameObject;

		infoBox = info.transform.GetChild(0).GetChild(0);

		infoBox.GetChild(0).GetComponent<Image>().sprite = player.playerFace;
		infoBox.GetChild(1).GetComponent<Text>().text = player.atributos.Classe.Nome + " Level: " + player.atributos.Level;

		infoBox.GetChild(2).GetChild(1).GetComponent<Text>().text = player.status.vidaString();
		infoBox.GetChild(3).GetChild(1).GetComponent<Text>().text = player.status.manaString();

		info.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Classe: " + player.classe;
		info.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = setAtributos(player);

		
	}
	private string setAtributos(Player player) {
		string atributos = null;
		
		atributos = "For: " + player.atributos.ForcaTotal + "   ";
		atributos += "Int: " + player.atributos.InteligenciaTotal + "   ";
		atributos += "Dano: " + player.atributos.DanoMedio + "\n";
		atributos += "Con: " + player.atributos.ConstituicaoTotal + "   ";
		atributos += "Des: " + player.atributos.DestrezaTotal + "   ";
		atributos += "ByteCoin: " + player.atributos.ByteCoin;

		return atributos;
	}
	GameObject instanciaObjeto(int posX, int posY, int width, int height, GameObject prefab, string nomePai) {
		GameObject objeto;
		Vector3 infoPosition = transform.position;
		objeto = Instantiate(prefab, infoPosition, Quaternion.identity) as GameObject;
		objeto.transform.SetParent(GameObject.Find(nomePai).transform);
		objeto.transform.localPosition = new Vector3(posX, posY, 1);
		objeto.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
		objeto.transform.localScale = new Vector3(1, 1, 1);
		
		objeto.name = prefab.name;
		return objeto;
	}
}
