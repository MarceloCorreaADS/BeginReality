using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Character;

public class CharacterMenu : MonoBehaviour {
	public Button button;
	private GameObject info;
	private List<Equipamento> equipamentos;
	private List<Habilidade> habilidades;
	private GameObject instanciaInfoBoxDesc;

	void Start() {
		button.onClick.AddListener(() => Show());
	}
	void Show() {
		habilidades = null;
		equipamentos = null;
		if (GameObject.Find("Canvas/PainelInventario/Menu/InfoBoxList") != null)
			Destroy(GameObject.Find("Canvas/PainelInventario/Menu/InfoBoxList"));
		SelectedPlayer selectedPlayer = SelectedPlayer.getInstance();
		Player player = selectedPlayer.getPlayer();
		instanciaInfoBoxDesc = instanciaObjeto(37, 0, 160, 150, Resources.Load<GameObject>("InfoBoxList"), GameObject.Find("Canvas/PainelInventario/Menu").transform);
		if (button.name == "Itens") {
			equipamentos = player.atributos.inventario.Equipamentos;
			
			if (equipamentos.Count > 0)
				listarEquipamentos(equipamentos);

		} else if (button.name == "Habilidades") {
			habilidades = new Habilidades(player).habilidades;
			if(habilidades.Count > 0)
				listarHabilidades(habilidades);
		}
	}
	void listarEquipamentos(List<Equipamento> equipamentos) {
		int count = 0;
		foreach (Equipamento e in equipamentos) {
			count++;
			instanciaObjetoLista(e, null, 0, -25 * count, 123, 25, Resources.Load<GameObject>("GenericButton"), instanciaInfoBoxDesc.transform.GetChild(0).GetChild(0).GetChild(0), count);
        }

	}
	void listarHabilidades(List<Habilidade> habilidades) {
		int count = 0;
		foreach (Habilidade h in habilidades) {
			count++;
			instanciaObjetoLista(null, h , 0,  -25 * count, 123, 25, Resources.Load<GameObject>("GenericButton"), instanciaInfoBoxDesc.transform.GetChild(0).GetChild(0).GetChild(0), count);
		}
	}
	void instanciaObjetoLista(Equipamento e, Habilidade h, int posX, int posY, int width, int height, GameObject prefab,Transform pai, int count) {
		GameObject objeto;
		objeto = Instancia(posX, posY, width, height, prefab, pai);
		objeto.AddComponent<CharacterLista>();
		if (e != null && h == null) {
			objeto.name = e.nome;
			objeto.GetComponent<CharacterLista>().equipamento = e;
			objeto.GetComponent<CharacterLista>().habilidade = null;
		} else if (h != null && e == null) {
			objeto.name = h.nome;
			objeto.GetComponent<CharacterLista>().habilidade = h;
			objeto.GetComponent<CharacterLista>().equipamento = null;
		}
		objeto.transform.GetChild(0).GetComponent<Text>().text = objeto.name;
		objeto.GetComponent<CharacterLista>().button = objeto.GetComponent<Button>();

        objeto.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(123, 24 + (25.25F * count));
	}
	GameObject instanciaObjeto(int posX, int posY, int width, int height, GameObject prefab, Transform pai) {
		GameObject objeto;
		objeto = Instancia(posX,posY,width,height,prefab,pai);

		objeto.name = prefab.name;
		objeto.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = button.name;
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
}	