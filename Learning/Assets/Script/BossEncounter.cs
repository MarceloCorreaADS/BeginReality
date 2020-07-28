using UnityEngine;

public class BossEncounter : MonoBehaviour {
	
	void OnCollisionEnter2D(Collision2D collision) {
		Character.ProgrammerMove.getInstance().Battle(Utils.BattleType.BOSS_BATTLE);
	}
}
