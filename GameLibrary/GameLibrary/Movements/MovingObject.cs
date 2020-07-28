using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Movements {
    public abstract class MovingObject : MonoBehaviour {
        //
        // Fields
        //
        public List<Vector2> endPos = new List<Vector2>();
        public List<Vector2> playerPos = new List<Vector2>();
        public int actualMove;
        public int qtyMove;
        public int endMove;
        public int quantMove = 0;


        private Rigidbody2D rb2D;
        private BoxCollider2D boxCollider;
        [HideInInspector]
        public Animator animator;
        public LayerMask blockingLayer;

        public float moveTime = 1f;
        private float inverseMoveTime;

        public bool action = false;
        public bool endTurn = false;
        Character.Player player;
        int positionX = 0;
        int positionY = 0;
		private ActionOrder actionOrder;
		private GameObject gameManager;
		protected Simulator simulator;


		//
		// Methods
		//
		protected virtual bool AttemptMove(int xDir, int yDir) {
            RaycastHit2D raycastHit2D;
            return this.Move(xDir, yDir, out raycastHit2D);
        }

        public void Move() {
            this.quantMove++;
            while (this.actualMove < this.qtyMove) {
                Vector2 vector = this.playerPos[this.actualMove];
                Vector2 vector2 = this.endPos[this.actualMove];
                if (this.qtyMove <= 0) {
                    break;
                }
                this.actualMove++;
				int order = actionOrder.addOrder();

				Task task = new Task(this.SmoothMovement(vector, vector2, order), true);
            }
        }

        protected IEnumerator SmoothMovement(Vector3 start, Vector3 end, int order) {
			yield return new WaitUntil(() => order == actionOrder.ActualOrder);
            this.action = false;
            this.position(start, end);
			int posX = positionX, posY = positionY;
			this.animator.SetBool("walk", true);
            this.animator.SetFloat("inputX", posX);
            this.animator.SetFloat("inputY", posY);
            float sqrMagnitude = (start - end).sqrMagnitude;
            while (sqrMagnitude > float.Epsilon) {
                Vector3 v = Vector3.MoveTowards(this.rb2D.position, end, this.inverseMoveTime * Time.deltaTime);
                this.rb2D.MovePosition(v);
                sqrMagnitude = (base.transform.position - end).sqrMagnitude;
                yield return null;
            }
            while ((Vector2) base.transform.position != (Vector2) end) {
                yield return null;
            }
            this.endMove++;
            this.action = true;
            this.animator.SetBool("walk", false);
			actionOrder.ActualOrder++;
            yield break;
        }

        public Vector2 getStart {
            get {
                if (this.qtyMove == 0) {
                    return base.transform.position;
                }
                return this.endPos[this.qtyMove - 1];
            }
        }

        public Vector2 getEnd(int x, int y, Vector2 start) {
            if (x == 0) {
                x = Convert.ToInt32(start.x);
                y = Convert.ToInt32(start.y + y);
            } else if (y == 0) {
                x = Convert.ToInt32(start.x + x);
                y = Convert.ToInt32(start.y);
            }
            return new Vector2((float) x, (float) y);
        }

        void settings(Vector2 start, Vector2 end) {
            setPlayer();
            if (player.status.mov > 0 && player.status.pontosAcao > 0) {
                int moveX = Convert.ToInt32(start.x - end.x);
                if (moveX < 0) {
                    moveX = -moveX;
                }
                int moveY = Convert.ToInt32(start.y - end.y);
                if (moveY < 0) {
                    moveY = -moveY;
                }
                int move = moveX + moveY;
                int pontosAcaoMove = move * 2;
                if (move > player.status.mov || pontosAcaoMove > player.status.pontosAcao) {
                    return;
                }

                this.player.status.mov -= move;
                this.player.status.pontosAcao -= pontosAcaoMove;
                Board.Grid grid = gameManager.GetComponent<Board.Grid>();
                grid.ChangeCharacterPosition(start, end);
                this.playerPos.Add(start);
                this.endPos.Add(end);
                this.qtyMove++;
            }
        }

        protected bool Move(int x, int y, out RaycastHit2D hit) {
            Vector2 start = getStart;
            Vector2 end = getEnd(x, y, start);
            this.boxCollider.enabled = false;
            hit = Physics2D.Linecast(start, end, this.blockingLayer);
            this.boxCollider.enabled = true;
            if (hit.transform == null || (hit.transform.tag == transform.tag && end != (Vector2) hit.transform.position)) {
                settings(start, end);
                return true;
            }
            return false;
        }

        protected virtual void IAMove(int x, int y) {
            Vector2 start = getStart;
            Vector2 end = getEnd(x, y, start);
            settings(start, end);
        }

        protected abstract void OnCantMove<T>(T component) where T : Component;

        void position(Vector2 start, Vector2 end) {
			positionX = 0;
			positionY = 0;
            if (start.x != end.x) {
                positionX = 1;
                if (start.x > end.x) {
                    positionX = -1;
                }
            } else {
                positionY = 1;
                if (start.y > end.y) {
                    positionY = -1;
                }
            }
        }

        protected virtual void Start() {
            this.boxCollider = base.GetComponent<BoxCollider2D>();
            this.rb2D = base.GetComponent<Rigidbody2D>();
            this.inverseMoveTime = 1f / this.moveTime;
			actionOrder = ActionOrder.getInstance();
			gameManager = GameManagerUtil.Instance.GameManager;
			simulator = Simulator.getInstance();
		}

        void setPlayer() {
            if (player == null) {
                this.player = base.GetComponent<Character.Player>();
            }
        }
    }
}
