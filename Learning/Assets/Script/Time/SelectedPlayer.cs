using UnityEngine;
using Character;

public class SelectedPlayer{
	private static SelectedPlayer instance;
	private Player player;
	private GameObject info = null;
	private SelectedPlayer() {}
	public static bool isUpdate = false;

	public GameObject getInfo() {
		if (info == null) {
            info = GameObject.Find("Canvas/PainelInventario/CharacterInfo");
		}
		return info;
	}

	public Player getPlayer() {
		return player;
	}
	public void setPlayer(Player playerSelecionado) {
		player = playerSelecionado;
	}
	public static SelectedPlayer getInstance() {
		if (instance == null) {
			instance = new SelectedPlayer();
		}
		return instance;
	}

}
