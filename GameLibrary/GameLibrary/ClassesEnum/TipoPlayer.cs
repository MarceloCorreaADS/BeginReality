using System;
using EnumUtils;

namespace ClassesEnum
{
	public class TipoPlayer {
		
		[AttributeUsage(AttributeTargets.Field)]
		public class ClasseInfo : Attribute {
			public string Nome { get; private set; }
			public string NomeGameObject { get; private set; }
			public int Forca {get; private set;}
			public int Constituicao {get; private set;}
			public int Destreza {get; private set;}
			public int Inteligencia {get; private set;}
            public int DistAtqMin { get; private set; }
            public int DistAtqMax { get; private set; }
            public int level { get; private set; }
            public int hp { get; private set; }
            public int sp { get; private set; }
            public TipoAtaque tipoAtaque { get; private set; }
			public bool isNPC { get; private set; }
            public bool isBoss { get; private set; }
			public int xp { get; private set; }

            public ClasseInfo(string nome, string nomeGameObject,TipoAtaque tipoAtaque ,int forca, int cons, int des, int intel, int distAtqMin, int distAtqMax, int level, int hp, int sp, bool isNPC = false, bool isBoss = false, int xp = 0) {
				this.Nome = nome;
				this.NomeGameObject = nomeGameObject;
                this.tipoAtaque = tipoAtaque;
                this.Forca = forca;
				this.Constituicao = cons;
				this.Destreza = des;
				this.Inteligencia = intel;
                this.DistAtqMin = distAtqMin;
                this.DistAtqMax = distAtqMax;
                this.level = level;
                this.hp = hp;
                this.sp = sp;
				this.isNPC = isNPC;
                this.isBoss = isBoss;
				this.xp = xp;
            }
		}
		
		public ClasseInfo getClasseInfo(ClasseEnum classeEnum) {
			return Utils.EnumExtensions.AttributeOf<ClasseInfo>(classeEnum);
		}
	}
}