using UnityEngine;
using System.Collections;

[System.Serializable]

public class GunController : MonoBehaviour {

    public GameObject shot;
    public Camera source;

    public float fireRate = 0.5f;
    private float nextFire = 0.0f;

    private AudioSource soundFire;

    // Use this for initialization
    void Start () {
        soundFire = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            Debug.Log("GunController script reacting to fire button press");
            nextFire = Time.time + fireRate;
            //Spawn slightly lower than the camera to ensure the one sided quad representing the shot is visible
            Vector3 shotSpawnPos = new Vector3(source.transform.position.x, source.transform.position.y - 0.5f, source.transform.position.z);
            Quaternion shotSpawnRot = source.transform.rotation;
            
            Instantiate(shot, shotSpawnPos, shotSpawnRot);

            soundFire.Play();
        }
    }
}
