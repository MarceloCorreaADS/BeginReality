using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Board {
	public class Pathfinding {
		public List<PathsList> pathsList = new List<PathsList>();
		public int minSize = int.MaxValue;
		public bool ready = false;

		public Pathfinding() {
		}

		public void FindPath(Vector2 start, Vector2 end) {
			Square startSquare = BoardSpec.getInstance().SquareFromWorldPoint(start);
			Square targetSquare = BoardSpec.getInstance().SquareFromWorldPoint(end);
			ready = false;
			new Utils.Task(Path(startSquare, targetSquare), true);
		}

		IEnumerator Path(Square startSquare, Square targetSquare) {
			List<Square> openSet = new List<Square>();
			List<Square> closedSet = new List<Square>();
			openSet.Add(startSquare);
			BoardSpec boardSpec = BoardSpec.getInstance();
			while (openSet.Count > 0) {
				Square currentSquare = openSet[0];
				for (int i = 1; i < openSet.Count; i++) {
					if (openSet[i].fCost < currentSquare.fCost || openSet[i].fCost == currentSquare.fCost && openSet[i].hCost < currentSquare.hCost) {
						currentSquare = openSet[i];
					}
				}

				openSet.Remove(currentSquare);
				closedSet.Add(currentSquare);

				if (currentSquare == targetSquare) {
					RetracePath(startSquare, targetSquare);
					yield break;
				}

				foreach (Square neighbour in boardSpec.GetNeighbours(currentSquare)) {
					if ((!neighbour.canWalk(startSquare.character) && (neighbour.character == null || (neighbour.character != targetSquare.character))) || closedSet.Contains(neighbour)) {
						continue;
					}

					int newMovementCostToNeighbour = currentSquare.gCost + BoardSpec.GetDistance(currentSquare, neighbour);
					if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
						neighbour.gCost = newMovementCostToNeighbour;
						neighbour.hCost = BoardSpec.GetDistance(neighbour, targetSquare);
						neighbour.parent = currentSquare;
						if (neighbour.hCost == 10 && neighbour.character != null) {
							continue;
						}
						if (!openSet.Contains(neighbour)) {
							openSet.Add(neighbour);
						}
					}
				}
				yield return null;
			}
		}

		void RetracePath(Square startSquare, Square endSquare) {
			List<Square> path = new List<Square>();
			Square currentSquare = endSquare;
			while (currentSquare != startSquare) {
				path.Add(currentSquare);
				currentSquare = currentSquare.parent;
			}
			path.Add(startSquare);
			path.Reverse();
			PathsList pathsList = new PathsList();
			pathsList.path = path;
			pathsList.size = path.Count;
			if (pathsList.size < minSize)
				minSize = pathsList.size;
			this.pathsList.Add(pathsList);
			ready = true;
		}

		public List<Vector2> getMoveInstructions() {
			PathsList pathsList = this.pathsList.Find(p => p.size == minSize);
			List<Vector2> moves = new List<Vector2>();
			int cont = 0;
			if (pathsList == null) {
				return moves;
			}
			foreach (Square square in pathsList.path) {
				cont++;
				if (cont >= minSize) {
					break;
				}
				Square nextSquare = pathsList.path[cont];
				int toX = 0, toY = 0;
				toX = (int) (-square.position.x + nextSquare.position.x);
				toY = (int) (-square.position.y + nextSquare.position.y);
				Vector2 vector2 = new Vector2(toX, toY);
				if (!nextSquare.hasCharacter || (nextSquare.hasCharacter && cont != minSize - 1)) {
					moves.Add(vector2);
				}
			}
			this.pathsList = new List<PathsList>();
			return moves;
		}
	}
	public class PathsList {
		public List<Square> path;
		public int size;
	}
}
