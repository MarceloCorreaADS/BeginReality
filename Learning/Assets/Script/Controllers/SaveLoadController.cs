using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Character;
using System.Collections.Generic;
using Utils;

public enum SaveTypes { PLAYER, ACCOMPLISHMENT, BATTLEREPORT, USERS, METHOD, TELA }
public class SaveLoadController {
	private static SaveLoadController instance;
	List<GameObject> gameObjects = null;
	private string user = UserLogged.GetInstance().userName;

	private SaveLoadController() { }

	public static SaveLoadController GetInstance() {
		if (instance == null)
			instance = new SaveLoadController();
		return instance;
	}

	public void SavePlayer()
    {
        setObjects();
        string tipo = SaveTypes.PLAYER.ToString().ToLower();
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		CreatePath();
		FileStream fileStream = File.Create("Data/" + user + "/" + tipo + ".dat");
		SavePlayers save = new SavePlayers();
		try {
			foreach (GameObject playerObject in gameObjects) {
				Player player = playerObject.GetComponent<Player>();
				if (player == null)
					continue;
				PlayerData playerData = new PlayerData();
				playerData.name = player.name;
				playerData.level = player.atributos.Level;
				playerData.byteCoin = player.atributos.ByteCoin;
				playerData.experiencia = player.atributos.Experiencia;
				playerData.classe = player.atributos.tipoClasse;
				List<Equipamento> equipamentos = new List<Equipamento>();
				Inventario inventario = player.atributos.inventario;
				equipamentos.Add(inventario.Elmo);
				equipamentos.Add(inventario.Armadura);
				equipamentos.Add(inventario.Arma);
				equipamentos.Add(inventario.Calca);
				equipamentos.Add(inventario.Bota);
				playerData.playerEquipment = equipamentos;
				save.players.Add(playerData);
			}
			binaryFormatter.Serialize(fileStream, save);
			fileStream.Close();
		} catch (Exception) {
			Debug.LogError("Não foi possivel salvar!");
		}
	}

	public void LoadPlayer() {
        setObjects();
		string tipo = SaveTypes.PLAYER.ToString().ToLower();
		if (File.Exists("Data/" + user + "/" + tipo + ".dat")) {
			SavePlayers savePlayer = LoadByTipo(tipo) as SavePlayers;
			try {
				foreach (GameObject playerObject in gameObjects) {
                    if (playerObject == null)
                        continue;

                    Player player = playerObject.GetComponent<Player>();
                    if (player == null)
						continue;

                    PlayerData playerData = savePlayer.players.Find(playerDat => playerDat.name == player.name);
                    if (playerData == null) {
                        continue;
                    }

                    player.Equipamentos = playerData.playerEquipment;
                    
                    player.atributos = new Atributos(playerData.level, playerData.experiencia, playerData.byteCoin, playerData.classe, playerData.playerEquipment);
				}
			} catch (Exception) {
				Debug.LogError("Não foi possivel Carregar!");
			}
		}
	}

	public void SaveMethods() {

	}

	public void LoadMethods() {

	}

	public void Save(SaveTypes saveType, object objeto) {
		string tipo = saveType.ToString().ToLower();
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		CreatePath();
		FileStream fileStream = File.Create("Data/" + user + "/" + tipo + ".dat");
		try {
			binaryFormatter.Serialize(fileStream, objeto);
			fileStream.Close();
		} catch (Exception) {
		}
	}

	public object Load(SaveTypes saveType) {
		string tipo = saveType.ToString().ToLower();
		object objeto = null;
		if (File.Exists("Data/" + user + "/" + tipo + ".dat")) {
			objeto = LoadByTipo(tipo);
		}
		return objeto;
	}

	private bool setObjects() {
		gameObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Ally"));
        if (gameObjects == null || gameObjects.Count == 0)
            return false;
		return true;
	}

	private void CreatePath() {
		if (UserLogged.GetInstance().userName != null && UserLogged.GetInstance().userName.Length > 0) {
			user = UserLogged.GetInstance().userName;
		}
		if (!Directory.Exists("Data/" + user)) {
			Directory.CreateDirectory("Data/" + user);
		}
	}

	private object LoadByTipo(string tipo) {
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileStream fileStream;
		try {
			fileStream = File.Open("Data/" + UserLogged.GetInstance().userName + "/" + tipo + ".dat", FileMode.Open);
		} catch (Exception) {
			fileStream = File.Open("Data/Usuarios/" + tipo + ".dat", FileMode.Open);
		}
		object objectLoaded = binaryFormatter.Deserialize(fileStream);
		fileStream.Close();
		return objectLoaded;
	}
}