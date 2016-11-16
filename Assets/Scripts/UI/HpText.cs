using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HpText : MonoBehaviour {
    [SerializeField]
    private Text textComponent;

    void Update() {
        textComponent.text = GameManager.Instance.Hp.ToString();
    }
}
