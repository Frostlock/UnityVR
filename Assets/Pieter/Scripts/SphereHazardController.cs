using UnityEngine;
using System.Collections;

public class SphereHazardController : MonoBehaviour {

    private AudioSource bounceSound;

    void Start ()
    {
        bounceSound = gameObject.GetComponent<AudioSource>();

        //Set random scale
        float s = Random.Range(0.25f, 2.5f);
        Vector3 spawnScale = new Vector3(s, s, s);
        gameObject.transform.localScale = spawnScale;
    }

    void OnCollisionEnter(Collision col)
    {
        bounceSound.Play();
    }
}
