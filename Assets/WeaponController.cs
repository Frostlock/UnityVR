using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

    public GameObject shot;
    public GameObject source;

    public float fireRate = 0.5f;
    private float nextFire = 0.0f;

    public float o;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TriggerClick()
    {
        nextFire = Time.time + fireRate;
        float offSetY = 0.25f;
        //Spawn slightly lower than the camera to ensure the one sided quad representing the shot is visible
        Vector3 shotSpawnPos = new Vector3(source.transform.position.x + source.transform.localPosition.x,
                                           source.transform.position.y + source.transform.localPosition.y + offSetY,
                                           source.transform.position.z + source.transform.localPosition.z);
        Quaternion shotSpawnRot = source.transform.rotation * Quaternion.Euler(0, -90, 0);
        Instantiate(shot, shotSpawnPos, shotSpawnRot);
    }
}
