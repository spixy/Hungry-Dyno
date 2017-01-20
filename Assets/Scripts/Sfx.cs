using UnityEngine;
using System.Collections;

public class Sfx : MonoBehaviour {

    [SerializeField]
    private AudioSource[] splatters;

    [SerializeField]
    private AudioSource slap;

    [SerializeField]
    private AudioSource pickup;


    public void Splatter() {
        AudioSource splat = splatters[Random.Range(0, splatters.Length - 1)];

        splat.Play();
    }

    public void Slap() {
        slap.Play();
    }

    public void Pickup() {
        pickup.Play();
    }

}
