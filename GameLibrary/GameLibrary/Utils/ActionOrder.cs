namespace Utils {
	public class ActionOrder {
		private static ActionOrder instance;
		private int actualOrder;
		private int qtyOrder;
		public static float waitTime {
			get {
				return 0.3F;
			}
		}

		public int ActualOrder {
			get {
				return actualOrder;
			}

			set {
				actualOrder = value;
			}
		}

		public int QtyOrder {
			get {
				return qtyOrder;
			}

			private set {
				qtyOrder = value;
			}
		}

		private ActionOrder() { }

		public static ActionOrder getInstance() {
			if (instance == null)
				instance = new ActionOrder();
			return instance;
		}

		public int addOrder() {
			int order = qtyOrder;
			qtyOrder++;
			return order;
		}

		public void resetActionOrder() {
			actualOrder = 0;
			qtyOrder = 0;
		}
	}
}