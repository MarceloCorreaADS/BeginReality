using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Character {
    public class Status : MonoBehaviour {
        public int maxVida;
        public int vida;
        public int maxMana;
        public int mana;
        public bool isDead = false;
        public bool updateLife = false;

        public int mov;
        public int acao;
        public int pontosAcao;
        public bool isBoos;
        public int order = 0;
        public bool attBars = false;
        public float hpScala;
        public float spScala;
        public GameObject InfoPlayer;
        Player player;

        void Start() {
            player = GetComponent<Player>();
            Atributos atributos = player.atributos;
            this.maxVida = atributos.hp;
            this.maxMana = atributos.sp;
            this.vida = maxVida;
            this.mana = maxMana;
            InfoPlayer = player.InfoPlayer;
            resetActionPoints();
            attBarras();
        }

        void OnMouseEnter() {
            player.GetComponent<EffectsPlayer>().showName(player.name, player.GetComponent<Transform>().position);
        }
        void OnMouseExit() {
            GameObject.Destroy(GameObject.Find("PersonName"));
        }

        public void resetActionPoints() {
            this.mov = 3;
            this.acao = 2;
            this.pontosAcao = 15;
        }

        public string vidaString() {
            return vida + "/" + maxVida;
        }

        public string manaString() {
            return mana + "/" + maxMana;
        }
        public void attBarras() {
            float sp = mana;
            float maxSp = maxMana;
            float hp = vida;
            float maxHp = maxVida;
            spScala = sp / maxSp;

            if (mana > 0)
                transformSp.transform.localScale = new Vector3(spScala, 1, 1);
            else
                transformSp.transform.localScale = new Vector3(0, 1, 1);

            textSp.text = manaString();

            hpScala = hp / maxHp;
            if (vida > 0)
                transformHp.transform.localScale = new Vector3(hpScala, 1, 1);
            else
                transformHp.transform.localScale = new Vector3(0, 1, 1);

            textHp.text = vidaString();
        }

        public bool UseSp(int sp) {
            if ((this.mana - sp) < 0) {
                attBarras();
                return false;
            } else {
                this.mana -= sp;
                return true;
            }
        }

        public void Damage(int damage) {
            this.vida -= damage;
            if (vida <= 0) {
                this.vida = 0;
                this.isDead = true;
            }

            attBarras();
            SoundManager.instance.Sfx("Damage");
            player.GetComponent<EffectsPlayer>().show(tipoEffect.DANO, damage.ToString(), player.GetComponent<Transform>().position);
        }

        public void Heal(int heal) {
            this.vida += heal;
            if (vida > maxVida) {
                this.vida = maxVida;
            }
            attBarras();
            SoundManager.instance.Sfx("Restore");
            player.GetComponent<EffectsPlayer>().show(tipoEffect.HP, heal.ToString(), player.GetComponent<Transform>().position);
        }

        public void Rejuvenate(int sp) {
            this.mana += sp;
            if (mana > maxMana) {
                this.mana = maxMana;
            }
            attBarras();
            SoundManager.instance.Sfx("Restore");
            player.GetComponent<EffectsPlayer>().show(tipoEffect.SP, sp.ToString(), player.GetComponent<Transform>().position);
        }

        private Text textHp {
            get {
                return this.InfoPlayer.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>();
            }
        }

        private Transform transformHp {
            get {
                return this.InfoPlayer.transform.GetChild(1).transform.GetChild(0).GetComponent<Transform>();
            }
        }

        private Text textSp {
            get {
                return this.InfoPlayer.transform.GetChild(2).transform.GetChild(1).GetComponent<Text>();
            }
        }

        private Transform transformSp {
            get {
                return this.InfoPlayer.transform.GetChild(2).transform.GetChild(0).GetComponent<Transform>();
            }
        }
    }
}