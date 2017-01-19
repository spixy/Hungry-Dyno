using System;
using UnityEngine;
using UnityEngine.UI;

public class EatCooldown : MonoBehaviour {
    [SerializeField]
    private Slider sliderComponent;

    [SerializeField]
    private Image sliderBackground;

    private int lastCd;

    private void Update() {
        if (GameManager.Instance.State != State.InGame)
            return;

        int cd = GameManager.Instance.dyno.AttackCd;

        if (cd != lastCd) {
            lastCd = cd;
            sliderComponent.value = cd;
        }
    }
}
