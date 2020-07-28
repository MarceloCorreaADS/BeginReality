using System.Collections.Generic;
using System;
using EnumUtils;

[Serializable]
class PlayerData {
	public string name;
	public int level;
	public int experiencia;
	public int byteCoin;
	public ClasseEnum classe;
	[UnityEngine.SerializeField]
	public List<Character.Equipamento> playerEquipment;
}
