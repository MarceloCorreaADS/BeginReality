using System;
using UnityEngine;
using System.Collections;

namespace Board {

	[System.Serializable]
	public class Square {
		public GameObject floor;
		public GameObject character;
		public GameObject walkingCharacter;
		public Vector2 position;
		public int weight = 1;
		public bool walk = true;
		// Distância do Inicio
		public int gCost;
		// Distância do Alvo
		public int hCost;
		public Square parent;

		public Square(GameObject floor, GameObject character, Vector2 position, int weight, bool walk) {
			this.floor = floor;
			this.character = character;
			this.position = position;
			this.weight = weight;
			this.walk = walk;
		}

		public int fCost {
			get {
				return gCost + hCost;
			}
		}

		public bool walkable {
			get {
				return character == null && walk;
			}
		}

		public bool hasCharacter {
			get {
				return character != null;
			}
		}

		public bool canWalk(GameObject character) {
			return (this.character == null || character != null && this.character.tag == character.tag) && walk;
		}
	}
}

