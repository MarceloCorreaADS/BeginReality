using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utils {
    public enum tipoEffect { SP, HP, DANO }
    public class EffectsPlayer : MonoBehaviour {
        public void show(tipoEffect tipo, string valor, Vector3 position) {
            GameObject prefab = null;
            GameObject instancia;
            int wEh = 0;
            if (tipo == tipoEffect.DANO) {
                List<GameObject> damages = new List<GameObject>();
                damages.Add(Resources.Load<GameObject>("Effects/DamageEffect1"));
                damages.Add(Resources.Load<GameObject>("Effects/DamageEffect2"));
                prefab = damages[(CalculosUtil.d(1, 2) - 1)];
                wEh = 50;
            } else if (tipo == tipoEffect.HP) {
                prefab = Resources.Load<GameObject>("Effects/HpEffect");
                wEh = 40;
            } else if (tipo == tipoEffect.SP) {
                prefab = Resources.Load<GameObject>("Effects/SpEffect");
                wEh = 40;
            }
            instancia = Instancia(position.x, position.y, wEh, wEh, prefab, GameObject.Find("Canvas").transform, valor.ToString());

            StartCoroutine(Destroy(instancia, instancia.GetComponent<Transform>().position.y));
        }
        private IEnumerator Destroy(GameObject instancia, float posIniY) {
            for (int i = 0; i < 10; i++) {
                instancia.GetComponent<Transform>().position = new Vector3(instancia.GetComponent<Transform>().position.x, instancia.GetComponent<Transform>().position.y + 0.1F);
                yield return new WaitForSeconds(0.1F);
            }
            GameObject.Destroy(instancia);

            yield break;
        }
        GameObject Instancia(float posX, float posY, int width, int height, GameObject prefab, Transform pai, string valor) {
            GameObject objeto;
            Vector3 infoPosition = transform.position;
            objeto = Instantiate(prefab, infoPosition, Quaternion.identity) as GameObject;
            objeto.transform.SetParent(pai);
            objeto.transform.position = new Vector3(posX, posY, 1);
            objeto.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            objeto.transform.localScale = new Vector3(1, 1, 1);

            objeto.name = prefab.name;
            objeto.transform.GetChild(0).GetComponent<Text>().text = valor.ToString();

            return objeto;
        }
        public void showName(string name, Vector3 position) {
            InstanciaName(position.x, position.y, 75, 20, Resources.Load<GameObject>("PersonName"), GameObject.Find("Canvas").transform, name);
        }
        void InstanciaName(float posX, float posY, int width, int height, GameObject prefab, Transform pai, string name) {
            GameObject objeto;
            Vector3 infoPosition = transform.position;
            objeto = Instantiate(prefab, infoPosition, Quaternion.identity) as GameObject;
            objeto.transform.SetParent(pai);
            objeto.transform.position = new Vector3(posX , posY + 0.5f, 1);
            objeto.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            objeto.transform.localScale = new Vector3(1, 1, 1);

            objeto.name = prefab.name;
            objeto.transform.GetComponent<Text>().text = name;
        }
    }
}
