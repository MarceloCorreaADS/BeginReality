using UnityEngine;
using System.Collections;

public class TeamManager : MonoBehaviour {
	void Start() {
		SaveLoadController.GetInstance().LoadPlayer();
	}
}