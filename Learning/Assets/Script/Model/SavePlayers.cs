using System;
using System.Collections.Generic;

[Serializable]
class SavePlayers {
	[UnityEngine.SerializeField]
	public List<PlayerData> players;
	public SavePlayers() {
		players = new List<PlayerData>();
	}
}
