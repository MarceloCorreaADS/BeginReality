using UnityEngine;
using Random = UnityEngine.Random;
using Utils;

namespace Character {

	public class ProgrammerMove : MonoBehaviour {

		private static ProgrammerMove instance = null;
		Rigidbody2D rigidBody;
		Animator animator;
		public float timeEncounter = 1.0f;
		public int randomChance = 2;
		bool isMoving;
		private float num = 0f;
		public Vector3 programmerPos;
		private bool blockMove = false;

		private ProgrammerMove() { }

		void Awake() {
			if (instance == null)
				instance = this;
			else if (instance != this)
				Destroy(gameObject);
			DontDestroyOnLoad(this);
		}

		void Start() {
			rigidBody = GetComponent<Rigidbody2D>();
			animator = GetComponent<Animator>();
		}

		void Update() {
			if (!blockMove) {
				Vector2 movimentVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
				isMoving = movimentVector != Vector2.zero;
				animator.SetBool("walk", isMoving);
				if (isMoving) {
					animator.StopPlayback();
					animator.Play("Walking");
					animator.SetFloat("inputX", movimentVector.x);
					animator.SetFloat("inputY", movimentVector.y);
					animator.speed = 0.5f;
				} else {
					StopAnimation();
				}
				programmerPos = transform.position;
				rigidBody.MovePosition(rigidBody.position + movimentVector * Time.deltaTime);
			}
		}

		void LateUpdate() {
			if (isMoving && !blockMove) {
				if (num < timeEncounter) {
					num += Time.deltaTime;
				}
				if (num >= timeEncounter) {
					num = 0f;
					int random = Random.Range(0, 10);
					if (random < randomChance) {
						Battle(BattleType.MINION_BATTLE);
					}
				}
			}
		}

		public static ProgrammerMove getInstance() {
			return instance;
		}
		public static void DestroyInstance() {
			Destroy(instance.gameObject);
			instance = null;
		}

		public void Battle(BattleType battleType) {
			StopAnimation();
			blockMove = true;
			Debug.Log("BATTLE!");
			UserLogged.GetInstance().battlesFought++;
			StartCoroutine(LoadScene.LoadBattleScene(battleType));
		}

		void StopAnimation() {
			animator.StopPlayback();
			animator.SetBool("walk", false);
			animator.Play("Idle");
			animator.speed = 0.3f;
		}

		public void setBlockMove(bool blockMove) {
			this.blockMove = blockMove;
		}
	}

}
