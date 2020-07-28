using System.Collections.Generic;
using System;

[Serializable]
public class BattleReportEntity {
	private List<string> playerActions;
	private int battleNumber;
	private String dateBattleStart;
	private String dateBattleEnd;
	private int map;
	private bool victory;
	private int qtyTurns;
	private bool bossBattle;
	private List<string> inimigos;

	public BattleReportEntity() {
		playerActions = new List<string>();
	}

	public List<string> PlayerActions {
		get {
			return playerActions;
		}

		set {
			playerActions = value;
		}
	}

	public int BattleNumber {
		get {
			return battleNumber;
		}

		set {
			battleNumber = value;
		}
	}

	public string DateBattleStart {
		get {
			return dateBattleStart;
		}

		set {
			dateBattleStart = value;
		}
	}

	public string DateBattleEnd {
		get {
			return dateBattleEnd;
		}

		set {
			dateBattleEnd = value;
		}
	}

	public int Map {
		get {
			return map;
		}

		set {
			map = value;
		}
	}

	public bool Victory {
		get {
			return victory;
		}

		set {
			victory = value;
		}
	}

	public int QtyTurns {
		get {
			return qtyTurns;
		}

		set {
			qtyTurns = value;
		}
	}

	public bool BossBattle {
		get {
			return bossBattle;
		}

		set {
			bossBattle = value;
		}
	}

	public List<string> Inimigos {
		get {
			return inimigos;
		}

		set {
			inimigos = value;
		}
	}
}

