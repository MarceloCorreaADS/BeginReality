using UnityEngine;
using System.Collections.Generic;
using Utils;

namespace Board {
	public class TileObjectList : ScriptableObject {
		public int col;
		public int row;
		public int qtyMinInimigos;
		public int qtyMaxInimigos;
		public int pecasByteCoin;
		public List<CharacterPos> charactersPos;
		public List<TileObject> tilesObject;
	}
	[System.Serializable]
	public class CharacterPos {
		public GameObject gameObject;
		public string tag;
		public int number;
		public int pecasByteCoin;
		public Vector3 startPosition;
		public NivelInimigo nivelDificuldadeFacil;
		public NivelInimigo nivelDificuldadeMedio;
		public NivelInimigo nivelDificuldadeDificil;

		public CharacterPos() {
			nivelDificuldadeDificil = new NivelInimigo();
			nivelDificuldadeFacil = new NivelInimigo();
			nivelDificuldadeMedio = new NivelInimigo();
		}

		public NivelInimigo GetNivelDificuldade() {
			if (ApplicationModel.Dificuldade == EnumUtils.Dificuldade.FACIL) {
				return nivelDificuldadeFacil;
			} else
			if (ApplicationModel.Dificuldade == EnumUtils.Dificuldade.MEDIO) {
				return nivelDificuldadeMedio;
			} else {
				return nivelDificuldadeDificil;
			}
		}
	}
	[System.Serializable]
	public class NivelInimigo {
		public int nivelMin;
		public int nivelMax;
	}
}
