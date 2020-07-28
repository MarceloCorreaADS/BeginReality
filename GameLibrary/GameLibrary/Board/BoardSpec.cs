using System.Collections.Generic;
using UnityEngine;
using System;

namespace Board {
	public class BoardSpec {
		private static BoardSpec instance;
		public int rows { get; private set; }
		public int columns { get; private set; }
		public List<Square> squares { get; private set; }

		private BoardSpec() { }

		public static BoardSpec getInstance() {
			if (instance == null)
				instance = new BoardSpec();
			return instance;
		}

		public void setRows(int a) {
			rows = a;
		}
		public void setColumns(int a) {
			columns = a;
		}
		public void setSquares(List<Square> squareList) {
			squares = squareList;
		}
		public static int GetDistance(Square squareA, Square squareB) {
			int dstX = Mathf.Abs((int) squareA.position.x - (int) squareB.position.x);
			int dstY = Mathf.Abs((int) squareA.position.y - (int) squareB.position.y);

			// Alterar calculo para 10 no começo, pois nao vai se mover na vertical
			if (dstX > dstY)
				return 10 * dstY + 10 * (dstX - dstY);
			return 10 * dstX + 10 * (dstY - dstX);
		}

		public static int GetSquareDistance(Square squareA, Square squareB) {
			int dstX = Mathf.Abs((int) squareA.position.x - (int) squareB.position.x);
			int dstY = Mathf.Abs((int) squareA.position.y - (int) squareB.position.y);
			return dstX + dstY;
		}

		public static int GetSquareDistance(Vector2 positionA, Vector2 positionB) {
			int dstX = Mathf.Abs((int) positionA.x - (int) positionB.x);
			int dstY = Mathf.Abs((int) positionA.y - (int) positionB.y);
			return dstX + dstY;
		}

		private List<Square> findNeighbours(Vector2 position, bool isY) {
			List<Square> neighbours = new List<Square>();
			List<int> list = new List<int>();
			list.Add(1);
			list.Add(-1);
			foreach (int fromList in list) {
				int x = fromList;
				int y = 0;
				if (isY) {
					y = x;
					x = 0;
				}
				x = (int) position.x + x;
				y = (int) position.y + y;

				if (x >= 0 && x < columns && y >= 0 && y < rows) {
					neighbours.Add(SquareFromWorldPoint(new Vector2(x, y)));
				}
			}
			return neighbours;
		}

		public Square SquareFromWorldPoint(Vector2 worldPosition) {
			int pos = (int) SquarePosWorldPoint(worldPosition);
			try {
				return squares[pos];
			} catch (Exception) {
				return null;
			}
		}

		public static float SquarePosWorldPoint(Vector2 worldPosition) {
			if (worldPosition.x < 0 || worldPosition.y < 0) {
				return -1;
			}
			float pos = (float) worldPosition.x * BoardSpec.getInstance().rows + (float) worldPosition.y;
			return pos;
		}

		public List<Square> GetNeighbours(Square square) {
			List<Square> neighbours = new List<Square>();
			neighbours.AddRange(findNeighbours(square.position, false));
			neighbours.AddRange(findNeighbours(square.position, true));
			return neighbours;
		}
	}
}
