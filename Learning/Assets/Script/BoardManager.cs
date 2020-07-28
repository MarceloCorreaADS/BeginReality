using UnityEngine;
using System;
using System.Collections.Generic;       //Allows us to use Lists.
using Random = UnityEngine.Random;      //Tells Random to use the Unity Engine random number generator.
using Utils;
using Board;
using Character;
using System.Linq;
using System.Collections;

public class BoardManager : MonoBehaviour {

	public GameObject[] enemyPrefabs;                                 //Array of enemy prefabs.
	public GameObject[] playerPrefabs;                                 //Array of player prefabs.
	public int qtyEnemies;
	public int qtyAlly;
	public List<Square> squares;
	TurnManager turnManager;

	//RandomPosition returns a random position from our list gridPositions.
	Vector3 RandomPosition(out int randomIndex) {
		randomIndex = 0;
		//Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.randomIndex = 0;
		while (squares.Count > 0) {
			randomIndex = Random.Range(0, squares.Count);
			if (squares[randomIndex].walkable) {
				return (Vector3) squares[randomIndex].position;
			}
		}
		return Vector3.zero;
	}


	//LayoutObjectAtRandom accepts an array of game objects to choose from along with a minimum and maximum range for the number of objects to create.
	void LayoutObjectAtRandom(GameObject tile, String name) {
		int index = 0;
		//Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
		Vector3 randomPosition = RandomPosition(out index);
		//Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
		GameObject player = Instantiate(tile, randomPosition, Quaternion.identity) as GameObject;
		player.name = name;
		squares[index].character = player;
		addTeamOnTurn(player.tag);
	}

	GameObject PutThere(GameObject tile, String name, Vector3 position) {
		GameObject player = Instantiate(tile, position, Quaternion.identity) as GameObject;
		player.name = name;
		int index = Convert.ToInt32(BoardSpec.SquarePosWorldPoint(position));
		squares[index].character = player;
		addTeamOnTurn(player.tag);
		return player;
	}

	void addTeamOnTurn(string tag) {
		if (turnManager != null && !turnManager.moves.Contains(tag)) {
			turnManager.moves.Add(tag);
		}
	}

	void PutEnemyThere(GameObject tile, String name, Vector3 position, int level, int byteCoin) {
		GameObject player = PutThere(tile, name, position);
		Player p = player.GetComponent<Player>();
		Atributos atributos = p.atributos;
		p.atributos = new Atributos(level, atributos.Experiencia, byteCoin, atributos.tipoClasse, atributos.inventario.Equipamentos);
	}

	void PutPlayerThere(GameObject tile, String name, Vector3 position) {
		GameObject player = PutThere(tile, name, position);
		Player p = player.GetComponent<Player>();
		Atributos atributos = p.atributos;
		p.atributos = new Atributos(3, 90, 3000, atributos.tipoClasse, atributos.inventario.Equipamentos);
	}

	public void SetupScene() {
		turnManager = GameManager.instance.GetComponent<TurnManager>();

		TileObjectList tileObjectList = Resources.Load<TileObjectList>(ApplicationModel.MapNamePath);
		if (tileObjectList != null)
			setPlayers(tileObjectList);
	}

	void setPlayers(TileObjectList tileObjectList) {
		List<CharacterPos> allys = tileObjectList.charactersPos.FindAll(characterPos => characterPos.tag == "Ally");
		foreach (CharacterPos characterPos in allys) {
			string name = new ClassesEnum.TipoPlayer().getClasseInfo(characterPos.gameObject.GetComponent<Player>().classe).Nome;
			PutThere(characterPos.gameObject, name, characterPos.startPosition);
			//PutPlayerThere(characterPos.gameObject, name, characterPos.startPosition);
		}
        new Task(loadPlayer(), true);

		List<CharacterPos> enemies = tileObjectList.charactersPos.FindAll(characterPos => characterPos.tag == "Enemy");
		int qty = tileObjectList.qtyMaxInimigos;
		if (tileObjectList.qtyMinInimigos > 0 && tileObjectList.qtyMinInimigos < tileObjectList.qtyMaxInimigos)
			qty = Random.Range(tileObjectList.qtyMinInimigos, tileObjectList.qtyMaxInimigos + 1);
		setEnemies(enemies, qty);
	}

    IEnumerator loadPlayer() {
        yield return new WaitForSeconds(0.2f);
        SaveLoadController.GetInstance().LoadPlayer();
        yield break;
    }

	void setEnemies(List<CharacterPos> enemies, int qtyEnemies) {
		int cont = 0;

		for (int i = 0; i < enemies.Count; i++) {
			CharacterPos temp = enemies[i];
			int randomIndex = Random.Range(i, enemies.Count);
			enemies[i] = enemies[randomIndex];
			enemies[randomIndex] = temp;
		}
		Dictionary<string, int> enemiesInGame = new Dictionary<string, int>();
		foreach (CharacterPos characterPos in enemies) {
			if (cont == qtyEnemies)
				break;
			string name = characterPos.gameObject.name;
			int count;
			try {
				count = enemiesInGame.First(e => e.Key == name).Value;
			} catch (Exception) {
				count = 0;
			}
			count++;
			NivelInimigo nivelInimigo = characterPos.GetNivelDificuldade();
			int level = Random.Range(nivelInimigo.nivelMin, nivelInimigo.nivelMax + 1);
			PutEnemyThere(characterPos.gameObject, name + count, characterPos.startPosition, level, characterPos.pecasByteCoin);
			enemiesInGame.Remove(name);
			enemiesInGame.Add(name, count);

		}
	}
}