using EnumUtils;
using Random = UnityEngine.Random;

namespace Utils {
    public static class CalculosUtil {

        public static int CalculoDano(int vezes, int dado, int DanoEquipamento, int modificadorPrincipal) {
            return d(vezes + modificadorPrincipal, (dado * vezes)) + modificadorPrincipal + DanoEquipamento;
        }

        public static int CalculoDanoMedio(int vezes, int dado, int DanoEquipamento, int modificadorPrincipal) {
            return ((vezes + modificadorPrincipal + (dado * vezes)) / 2) + modificadorPrincipal + DanoEquipamento;
		}

		public static int CalculoDado(int vezes, int dado) {
			return d(vezes, (dado * vezes));
		}

		public static int CalculoDadoMedio(int vezes, int dado) {
			return (vezes + (dado * vezes)) / 2;
		}

        public static int d(int min, int max) {
            return Random.Range(min, max);
        }
        public static int CA(int CAEquipamento, int DestrezaTotal, int CAExtra) { // Classe de Armadura
            return 10 + CAEquipamento + ModificadorAtributo(DestrezaTotal) + CAExtra;
        }

        public static int CalculoAcerto(int BAB, int AtributoTotal, int BAE) {
            return BAB + ModificadorAtributo(AtributoTotal) + BAE;
        }

        public static int AcertoBase(Character.Atributos atributos) {
            if (atributos.Classe.tipoAtaque == TipoAtaque.CORPO_A_CORPO) {
                return CalculoAcerto(atributos.BAB, atributos.ForcaTotal, atributos.BAE);
            } else if (atributos.Classe.tipoAtaque == TipoAtaque.ATAQUE_A_DISTANCIA || atributos.Classe.tipoAtaque == TipoAtaque.MAGICO) {
                return CalculoAcerto(atributos.BAB, atributos.DestrezaTotal, atributos.BAE);
            }
            return 0;
        }

        public static int CalculaPontosDeAtaque(Character.Atributos atributos) {
            if (atributos.Classe.tipoAtaque == TipoAtaque.CORPO_A_CORPO) {
                return atributos.BAB + ModificadorAtributo(atributos.ForcaTotal) + atributos.BAE;
            } else if (atributos.Classe.tipoAtaque == TipoAtaque.ATAQUE_A_DISTANCIA || atributos.Classe.tipoAtaque == TipoAtaque.MAGICO) {
                return atributos.BAB + ModificadorAtributo(atributos.DestrezaTotal) + atributos.BAE;
            }
            return 0;
        }

        public static int ModificadorAtributo(int Atributo) {
            return (Atributo - 10) / 2;
        }

        public static int CalculoPontosAcaoMovimento(TipoAtaque tipoAtaque, int move) {
            int bonus = 0;
            if (TipoAtaque.CORPO_A_CORPO == tipoAtaque && move > 4) {
                bonus = move - 4;
                move = 4;
            } else if (tipoAtaque != TipoAtaque.CORPO_A_CORPO && move > 2) {
                bonus = move - 2;
                move = 2;
            }
            return move * 2 + bonus * 3;
        }

    }
}
