using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HpText : MonoBehaviour {
    [SerializeField]
    private Text textComponent;

    void Update() {
        int hp = (int) GameManager.Instance.Hp;
        textComponent.text = "HP: " + hp.ToString();
    }
}
