using UnityEngine;
using System.Collections;

public class CubeController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider other)
    {
        //Trigger is a MeleeWeapon
        if (other.gameObject.CompareTag("MeleeWeapon"))
        {
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            //Remove current forces from the object (preventing continued movement after contact)
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
}
