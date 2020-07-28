using UnityEngine;
using UnityEngine.UI;

public enum TipoData{SAVE, LOAD};

public class SaveLoadGameButton : MonoBehaviour
{
    public TipoData tipoData;
    Button btn;

    void Awake()
    {
        btn = GetComponent<Button>();
    }

    // Use this for initialization
    void Start()
    {
        btn.onClick.AddListener(() => MyMethod());
    }

    public void MyMethod()
    {
        if (tipoData == TipoData.SAVE) {
            SaveLoadController.GetInstance().SavePlayer();
        } else if(tipoData == TipoData.LOAD) {
            SaveLoadController.GetInstance().LoadPlayer();
        }
    }
}
