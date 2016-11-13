using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    [SerializeField]
    private int damage = 1;

    public void Eat() {
        Debug.Log("EATEN");
        Destroy(gameObject);
    }
}
