using UnityEngine;
using System.Collections;

public class FruitController : MonoBehaviour {

    public GameObject explosion;
    public GameObject slice;
    public GameObject sliceEffect;

    void OnTriggerEnter(Collider other)
    {
        //Trigger is a bolt
        if (other.gameObject.CompareTag("Bolt"))
        {
            Instantiate(explosion, transform.position, other.transform.rotation);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        //Trigger is a MeleeWeapon
        if (other.gameObject.CompareTag("MeleeWeapon"))
        {
            Vector3 positionOffset;
            positionOffset = other.transform.right;
            positionOffset.Scale(new Vector3(0.02f, 0.02f, 0.02f));

            //Spawn slices
            Quaternion sliceRotation = other.transform.rotation;
            GameObject slice1 = (GameObject) Instantiate(slice, transform.position - positionOffset, sliceRotation);
            GameObject slice2 = (GameObject) Instantiate(slice, transform.position + positionOffset, sliceRotation);
            slice2.transform.Rotate(0f, 180f,0f);

            //Spawn slice effect
            Instantiate(sliceEffect, transform.position, transform.rotation);

            //Destroy original fruit
            Destroy(gameObject);
        }
    }

}
