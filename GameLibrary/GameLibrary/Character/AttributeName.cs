using EnumUtils;
using System;

namespace Character {
	public class AttributeName : Attribute {
		public string nameOriginal { get; private set; }
		public string name { get; private set; }
		public ClasseEnum Classe { get; private set; }
		public TipoEquipamento Item { get; private set; }
		public int min { get; private set; }
		public int max { get; private set; }
		public AttributeName(string nameOriginal, string name, ClasseEnum Classe, TipoEquipamento Item, int min = 0, int max = 0) {
			this.nameOriginal = nameOriginal;
			this.name = name;
			this.Classe = Classe;
			this.Item = Item;
			this.min = min;
			this.max = max;
		}
	}
}
