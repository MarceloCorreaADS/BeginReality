using ClassesEnum;
using EnumUtils;
using UnityEngine;

namespace Character {
	public class DistribuidorAtributos {
		private static float pointsPerLevel;
		private static int totalPoints;

		public static AtributosBase distribuirPontosMinion(int level, AtributosBase atributosBase, TipoPlayer.ClasseInfo classeInfo) {
			pointsPerLevel = 1.5f;
			totalPoints = Mathf.FloorToInt(pointsPerLevel * level);
			atributosBase = distribuirPontos(atributosBase, classeInfo);
			return atributosBase;
		}
		public static AtributosBase distribuirPontosBoss(int level, AtributosBase atributosBase, TipoPlayer.ClasseInfo classeInfo) {
			pointsPerLevel = 3f;
			totalPoints = Mathf.FloorToInt(pointsPerLevel * level);
			atributosBase = distribuirPontos(atributosBase, classeInfo);
			return atributosBase;
		}
		public static AtributosBase distribuirPontosPlayer(int level, AtributosBase atributosBase, TipoPlayer.ClasseInfo classeInfo) {
			pointsPerLevel = 1f;
			totalPoints = Mathf.FloorToInt(pointsPerLevel * level);
			atributosBase = distribuirPontos(atributosBase, classeInfo);
			return atributosBase;
		}

		private static AtributosBase distribuirPontos(AtributosBase atributosBase, TipoPlayer.ClasseInfo classeInfo) {
			AtributoPref[] atributosPref;
			int totalAtributos = atributosBase.totalAtributos();
			int forca = atributosBase.Forca;
			int constituicao = atributosBase.Constituicao;
			int destreza = atributosBase.Destreza;
			int inteligencia = atributosBase.Inteligencia;
			// Adiciona os AtributosPref na ordem de preferencia.
			if (classeInfo.tipoAtaque.Equals(TipoAtaque.CORPO_A_CORPO)) {
				atributosPref = new AtributoPref[] { AtributoPref.FORCA, AtributoPref.CONSTITUICAO, AtributoPref.DESTREZA, AtributoPref.INTELIGENCIA };
			} else if (classeInfo.tipoAtaque.Equals(TipoAtaque.ATAQUE_A_DISTANCIA)) {
				atributosPref = new AtributoPref[] { AtributoPref.DESTREZA, AtributoPref.INTELIGENCIA, AtributoPref.CONSTITUICAO };
			} else {
				atributosPref = new AtributoPref[] { AtributoPref.INTELIGENCIA, AtributoPref.DESTREZA, AtributoPref.CONSTITUICAO};
				totalAtributos = inteligencia + destreza + constituicao;
			}
			// Distribui os pontos
			while (totalPoints > 0) {
				foreach (AtributoPref atributoPref in atributosPref) {
					if (atributoPref.Equals(AtributoPref.FORCA)) {
						forca += pointsTo(totalAtributos, forca);
					} else if (atributoPref.Equals(AtributoPref.CONSTITUICAO)) {
						constituicao += pointsTo(totalAtributos, constituicao);
					} else if (atributoPref.Equals(AtributoPref.DESTREZA)) {
						destreza += pointsTo(totalAtributos, destreza);
					} else if (atributoPref.Equals(AtributoPref.INTELIGENCIA)) {
						inteligencia += pointsTo(totalAtributos, inteligencia);
					}
					if (totalPoints == 0)
						break;
				}
			}
			
			return new AtributosBase(forca, constituicao, destreza, inteligencia);
		}

		private static int pointsTo(int totalAtribute, int atributeValue) {
			float atributePercentage = calculatePercentage(totalAtribute, atributeValue);
			int pontosGastos = Mathf.CeilToInt(atributePercentage * totalPoints / 100);
			while (pontosGastos > totalPoints) {
				pontosGastos--;
			}
			totalPoints -= pontosGastos;
			return pontosGastos;
		}

		private static float calculatePercentage(int total, float value) {
			return 100 * value / total;
		}
	}
}
