using UnityEngine;
using UnityEngine.UI;

public class HpText : MonoBehaviour {
    [SerializeField]
    private Text textComponent;

    private Dyno dyno;

    void Start()
    {
        this.dyno = GameManager.Instance.dyno;
    }

    void Update() {
        textComponent.text = "HP: " + (int)dyno.Hp;
    }
}
