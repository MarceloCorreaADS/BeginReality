using EnumUtils;

namespace Utils {
	public enum BattleType { BOSS_BATTLE, MINION_BATTLE }
	public enum MapToLoadType { NORMAL, NEXT, PREVIUS }
	public class ApplicationModel {
		private static int currentWorldMap = 1;
		private static BattleType battleType = BattleType.MINION_BATTLE;
		private static Dificuldade dificuldade = Dificuldade.FACIL;
		private static bool endBattle = false;
		private static bool victory = false;
		private static MapToLoadType mapToLoadType = MapToLoadType.NORMAL;

		public static string BattleTypeName {
			get {
				if (BattleType.BOSS_BATTLE.Equals(battleType))
					return "Boss";
				return "Minion";
			}
		}
		public static string MapNamePath {
			get {
				return "Map/" + BattleTypeName + currentWorldMap;
			}
		}

		public static int CurrentWorldMap {
			get {
				return currentWorldMap;
			}

			set {
				currentWorldMap = value;
			}
		}

		public static BattleType BattleType {
			get {
				return battleType;
			}

			set {
				battleType = value;
			}
		}

		public static Dificuldade Dificuldade {
			get {
				return dificuldade;
			}

			set {
				dificuldade = value;
			}
		}

		public static bool EndBattle {
			get {
				return endBattle;
			}

			set {
				endBattle = value;
			}
		}

		public static bool Victory {
			get {
				return victory;
			}

			set {
				victory = value;
			}
		}

		public static MapToLoadType MapToLoadType {
			get {
				return mapToLoadType;
			}

			set {
				mapToLoadType = value;
			}
		}

		public static void SetBattleInfoDefaultOptions() {
			battleType = BattleType.MINION_BATTLE;
			endBattle = false;
			victory = false;
		}
		public static void BalanceDificult() {
			if (CurrentWorldMap == 1 || CurrentWorldMap == 2)
				Dificuldade = Dificuldade.FACIL;
			if (CurrentWorldMap == 3 || CurrentWorldMap == 4)
				Dificuldade = Dificuldade.MEDIO;
			if (CurrentWorldMap == 5)
				Dificuldade = Dificuldade.DIFICIL;
		}
	}
}
