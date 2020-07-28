using UnityEngine;
using System;

namespace Utils {
    public enum TipoSimulacao { ATAQUE, MOVIMENTO }
    public class Simulator {
		private static Simulator instance;
		private int qtySimulacoes;

		private Simulator() { }

		public int QtySimulacoes {
			get {
				return qtySimulacoes;
			}

			set {
				qtySimulacoes = value;
			}
		}

		public static Simulator getInstance() {
			if (instance == null)
				instance = new Simulator();
			return instance;
		}

        public void Check(TipoSimulacao tipoSimulacao, bool check) {
            if (QtySimulacoes == 0 || check)
                return;
            try {
                switch (tipoSimulacao) {
                    case TipoSimulacao.ATAQUE:
                        ConsoleReport.LogBattle("Alvo não está no alcance.");
                        break;
                    case TipoSimulacao.MOVIMENTO:
                        ConsoleReport.LogBattle("Local obstruido, não foi possível se mover");
                        break;
                }
                QtySimulacoes = 0;
            } catch (Exception) {
                Debug.Log("Erro ao achar ConsoleButtons.");
            }
        }
    }
}
