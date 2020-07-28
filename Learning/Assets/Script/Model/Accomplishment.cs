using System;
using System.Collections.Generic;

[Serializable]
public class Accomplishment {
	[UnityEngine.SerializeField]
	public List<AccomplishmentInfo> accomplishmentInfos;

	public bool checkBossDead(int map) {
		bool isBossDead = false;
		if (accomplishmentInfos != null && accomplishmentInfos.Count > 0) {
			AccomplishmentInfo accomplishmentInfo = accomplishmentInfos.Find(ai => ai.Map == map);
			if (accomplishmentInfo != null) {
				isBossDead = accomplishmentInfo.BossDead;
			}
		}
		return isBossDead;
	}

	public bool checkTerminalActivated(int map) {
		bool isTerminalActivated = false;
		if (accomplishmentInfos != null && accomplishmentInfos.Count > 0) {
			AccomplishmentInfo accomplishmentInfo = accomplishmentInfos.Find(ai => ai.Map == map);
			if (accomplishmentInfo != null) {
				isTerminalActivated = accomplishmentInfo.TerminalActivated;
			}
		}
		return isTerminalActivated;
	}

	public void setBossDead(int map) {
		AccomplishmentInfo accomplishmentInfo = new AccomplishmentInfo(map, true);
		if (accomplishmentInfos == null)
			accomplishmentInfos = new List<AccomplishmentInfo>();
		accomplishmentInfos.Add(accomplishmentInfo);
	}

	public void setTerminalActivated(int map) {
		int index = accomplishmentInfos.FindIndex(ai => ai.Map == map);
		accomplishmentInfos[index].TerminalActivated = true;
	}
}
