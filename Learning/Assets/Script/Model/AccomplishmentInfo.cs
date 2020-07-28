using System;

[Serializable]
public class AccomplishmentInfo {
	private int map;
	private bool bossDead;
	private bool terminalActivated;

	public AccomplishmentInfo() { }
	public AccomplishmentInfo(int map, bool bossDead) {
		this.map = map;
		this.bossDead = bossDead;
	}

	public int Map {
		get {
			return map;
		}

		set {
			map = value;
		}
	}

	public bool BossDead {
		get {
			return bossDead;
		}

		set {
			bossDead = value;
		}
	}

	public bool TerminalActivated {
		get {
			return terminalActivated;
		}

		set {
			terminalActivated = value;
		}
	}
}