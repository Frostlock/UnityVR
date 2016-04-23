using UnityEngine;
using System.Collections;

public class ImpactHandler : MonoBehaviour {

    public GameObject explosion;

    void OnTriggerEnter(Collider other)
    {
        //Trigger is a bolt
        if (other.gameObject.CompareTag("Bolt"))
        {
            //In case of boundary object destroy the bullet
            if (gameObject.CompareTag("Boundary"))
            {
                Instantiate(explosion, other.transform.position, other.transform.rotation);
                Destroy(other.gameObject);
            }

            //In case of hazard object destroy the bullet and the hazard
            if (gameObject.CompareTag("Hazard"))
            {
                Instantiate(explosion, other.transform.position, other.transform.rotation);
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
        }
        //Trigger is a MeleeWeapon
        if (other.gameObject.CompareTag("MeleeWeapon"))
        {
            //In case of boundary object meleeweapon should get stuck
            if (gameObject.CompareTag("Boundary"))
            {
                other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            }

            //In case of hazard object destroy the hazard
            if (gameObject.CompareTag("Hazard"))
            {
                Instantiate(explosion, other.transform.position, other.transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    /*
    void OnTriggerStay(Collider other)
    {
        //Trigger is a MeleeWeapon
        if (other.gameObject.CompareTag("MeleeWeapon"))
        {
            //In case of boundary object meleeweapon should get stuck
            if (gameObject.CompareTag("Boundary"))
            {
                other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            }
        }
    }
    */

}
