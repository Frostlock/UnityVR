using UnityEngine;
using System.Collections;

public class SphereHazardController : MonoBehaviour {

    private AudioSource bounceSound;

    public float minScale = 0.25f;
    public float maxScale = 2.5f;

    void Start ()
    {
        bounceSound = gameObject.GetComponent<AudioSource>();

        //Set random scale
        float s = Random.Range(minScale, maxScale);
        Vector3 spawnScale = new Vector3(s, s, s);
        gameObject.transform.localScale = spawnScale;
    }

    void OnCollisionEnter(Collision col)
    {
        bounceSound.Play();
    }
}
