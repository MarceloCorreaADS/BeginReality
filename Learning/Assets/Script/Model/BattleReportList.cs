using System.Collections.Generic;
using System;

[Serializable]
public class BattleReportList {
	List<BattleReportEntity> battleReportEntityList;

	public BattleReportList() {
		battleReportEntityList = new List<BattleReportEntity>();
	}

	public List<BattleReportEntity> BattleReportEntityList {
		get {
			return battleReportEntityList;
		}

		set {
			battleReportEntityList = value;
		}
	}

	public BattleReportEntity getByBattleNumber(int battleNumber) {
		return BattleReportEntityList.Find(bre => bre.BattleNumber == battleNumber);
	}

	public void addPosition(int battleNumber, BattleReportEntity battleReportEntity) {
		int index = BattleReportEntityList.FindIndex(bre => bre.BattleNumber == battleNumber);
		BattleReportEntityList[index] = battleReportEntity;
	}
}
