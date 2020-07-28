using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using Utils;

namespace Board {
    public class Grid : MonoBehaviour {
        public Grid() {
        }
        public int columns = 12;                                         //Number of columns in our game board.
        public int rows = 8;                                             //Number of rows in our game board.
        public GameObject[] floorTiles;                                 //Array of floor prefabs.
        public GameObject[] outerWallTiles;
        public TileObjectList tileObjectList;

        private Transform boardHolder;                                  //A variable to store a reference to the transform of our Board object.
        [UnityEngine.SerializeField]
        public List<Square> squares = new List<Square>();

        //Sets up the outer walls and floor (background) of the game board.
        public void BoardSetup() {
            squares.Clear();
            //Instantiate Board and set boardHolder to its transform.
            boardHolder = GameObject.Find("Board").transform;
			tileObjectList = Resources.Load<TileObjectList>(ApplicationModel.MapNamePath);
            if (tileObjectList != null) {
                columns = tileObjectList.col;
                rows = tileObjectList.row;
            }
			BoardSpec boardSpec = BoardSpec.getInstance();
			boardSpec.setRows(rows);
			boardSpec.setColumns(columns);
            int cont = 0;
            //Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
            for (int x = -1; x < columns + 1; x++) {
                //Loop along y axis, starting from -1 to place floor or outerwall tiles.
                for (int y = -1; y < rows + 1; y++) {
                    //Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
                    TileObject tileObject;
                    GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                    bool walkable = true;

                    //Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
                    string name = "Wall";
                    bool addSquare = true;
                    Vector3 scale = new Vector3(0.98F, 0.98F);
                    if (x == -1 || x == columns || y == -1 || y == rows) {
                        toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                        name = "Outer Wall";
                        addSquare = false;
                        scale = new Vector3(1F, 1F);
                    } else if (tileObjectList != null) {
                        tileObject = tileObjectList.tilesObject[cont];
                        toInstantiate = tileObject.tile;
                        walkable = tileObject.walkable;
                        float scaleMultiplier = toInstantiate.GetComponent<BlockPos>().scaleMultiplier;
                        float ajuste = 0;
                        if (scaleMultiplier > 1)
                            ajuste = 0.1f;
                        if (scaleMultiplier != 1)
                            scale = new Vector3((scaleMultiplier + ajuste) * 0.98f, (scaleMultiplier + ajuste) * 0.98f);
                    }

                    //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
                    GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                    instance.transform.localScale = scale;
                    instance.name = name + " x: " + x + ", y: " + y;
                    //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
                    instance.transform.SetParent(boardHolder);
                    if (addSquare) {
                        squares.Add(new Square(instance, null, new Vector2(x, y), 1, walkable));
                        cont++;
                    }
                }
            }
			boardSpec.setSquares(squares);
        }



        public void ChangeCharacterPosition(Vector2 start, Vector2 end) {
			BoardSpec boardSpec = BoardSpec.getInstance();
			Square squareStart = boardSpec.SquareFromWorldPoint(start);
            Square squareEnd = boardSpec.SquareFromWorldPoint(end);
            // Walking character seria um personagem que está passando pelo Square
            // Se existir um character no campo, o segundo deve apenas passar por ele 
            // e deve ser retirado do campo no proximo movimento
            // No caso do NPC, todo quadrado é um movimento.
            // No caso do player, cada movimento tem um número de 1-x de quadrados a andar
            // em uma direção. Se no final dessa direção possuir 1 character no lugar
            // ele não pode se mover para lá.
            if (squareEnd != null && squareStart != null) {
                GameObject character = squareStart.character;
                if (squareStart.walkingCharacter != null) {
                    character = squareStart.walkingCharacter;
                }
                if (squareEnd.character != null) {
                    squareEnd.walkingCharacter = character;
                } else {
                    squareEnd.character = character;
                }
                if (squareStart.walkingCharacter != null) {
                    squareStart.walkingCharacter = null;
                } else {
                    squareStart.character = null;
                }
            }
        }

        public GameObject getCharacterObject(float x, float y) {
            return getCharacterObject(new Vector2(x, y));
        }

        public GameObject getCharacterObject(Vector2 vector2) {
            Square pos = BoardSpec.getInstance().SquareFromWorldPoint(vector2);
            try {
                return pos.character;
            } catch (ArgumentNullException) {
                Debug.Log("Grid - getCharacterObject - argument null exception");
                return null;
            } catch (NullReferenceException) {
                Debug.Log("Grid - getCharacterObject - null reference exception");
                return null;
            }
        }

        public Square getNearCharacter(Vector2 startPos, string tag) {
            Square startSquare = BoardSpec.getInstance().SquareFromWorldPoint(startPos);
            Square finalSquare = null;

            foreach (GameObject character in GameObject.FindGameObjectsWithTag(tag)) {
                List<Square> openSet = new List<Square>();
                List<Square> closedSet = new List<Square>();
                openSet.Add(startSquare);
				Character.Player player = character.gameObject.GetComponent<Character.Player>();
                if (player.status.vida <= 0) {
                    continue;
                }
                Square targetSquare = BoardSpec.getInstance().SquareFromWorldPoint(character.transform.position);
                if (targetSquare == null)
                    continue;
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
                        if (closedSet.Count > 1) {
                            Square nearLastSquare = closedSet[closedSet.Count - 1];
                            if (finalSquare == null || nearLastSquare.fCost < finalSquare.fCost || (nearLastSquare.fCost == finalSquare.fCost && nearLastSquare.hCost < finalSquare.hCost)) {
                                finalSquare = nearLastSquare;
                            }
                        }
                        break;
                    }

                    foreach (Square neighbour in BoardSpec.getInstance().GetNeighbours(currentSquare)) {
                        if (!neighbour.canWalk(startSquare.character) && (neighbour.character == null || neighbour.character != targetSquare.character) || closedSet.Contains(neighbour)) {
                            continue;
                        }

                        int newMovementCostToNeighbour = currentSquare.gCost + BoardSpec.GetDistance(currentSquare, neighbour);
                        if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
                            neighbour.gCost = newMovementCostToNeighbour;
                            neighbour.hCost = BoardSpec.GetDistance(neighbour, targetSquare);

                            if (!openSet.Contains(neighbour)) {
                                openSet.Add(neighbour);
                            }
                        }
                    }
                }
            }
            return finalSquare;
        }
    }
}