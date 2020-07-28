using System;
using UnityEngine;
using System.Collections.Generic;

namespace Movements {
    public class Move : MovingObject {
        //
        // Methods
        //
        protected override bool AttemptMove(int xDir, int yDir) {

            bool canMove = base.AttemptMove(xDir, yDir);
			simulator.Check(Utils.TipoSimulacao.MOVIMENTO, canMove);
            return canMove;
        }

        public bool MoveDown(int vertical) {
            return vertical > 0 && this.AttemptMove(0, -vertical);
        }

        public bool MoveLeft(int horizontal) {
            return horizontal > 0 && this.AttemptMove(-horizontal, 0);
        }

        public bool MoveRight(int horizontal) {
            return horizontal > 0 && this.AttemptMove(horizontal, 0);
        }

        public bool MoveUp(int vertical) {
            return vertical > 0 && this.AttemptMove(0, vertical);
        }

        public void NpcMove(int vertical, int horizontal) {
            base.IAMove(vertical, horizontal);
        }

        protected override void OnCantMove<T>(T component) {
        }

        protected override void Start() {
            this.animator = base.GetComponent<Animator>();
            base.Start();
            this.action = true;
        }

        public void resetParameters(String check) {
            this.action = true;
            this.endTurn = false;
            this.actualMove = 0;
            this.qtyMove = 0;
            this.endMove = 0;
            this.quantMove = 0;
            this.action = true;
            this.endPos = new List<Vector2>();
            this.playerPos = new List<Vector2>();
        }
    }
}
