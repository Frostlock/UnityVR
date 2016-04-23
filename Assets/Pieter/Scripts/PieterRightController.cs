using UnityEngine;
using System.Collections;

public class PieterRightController : MonoBehaviour {

    private Transform teleportReference;

    private GameObject touchedObject = null;
    private GameObject heldObject = null;

    // Use this for initialization
    void Start()
    {
        if (GetComponent<SteamVR_TrackedController>() == null)
        {
            Debug.LogError("Script must be on a SteamVR_TrackedController");
            return;
        }

        Transform eyeCamera = GameObject.FindObjectOfType<SteamVR_Camera>().GetComponent<Transform>();
        // The referece point for the camera is two levels up from the SteamVR_Camera
        teleportReference = eyeCamera.parent.parent;

        //Register event handlers
        if (GetComponent<SteamVR_TrackedController>() == null)
        {
            Debug.LogError("SteamVR_Teleporter must be on a SteamVR_TrackedController");
            return;
        }
        GetComponent<SteamVR_TrackedController>().TriggerClicked += new ClickedEventHandler(TriggerClick);
        GetComponent<SteamVR_TrackedController>().MenuButtonClicked += new ClickedEventHandler(MenuClick);
        GetComponent<SteamVR_TrackedController>().PadClicked += new ClickedEventHandler(PadClick);

    }

    
    void Update()
    {
        //SteamVR_Controller.Input(1).TriggerHapticPulse(1000,Valve.VR.EVRButtonId.k_EButton_Axis0);
        if (touchedObject != null)
        {
            //Trigger a haptic pulse on the controller
            int controllerIndex;
            controllerIndex = (int)GetComponent<SteamVR_TrackedController>().controllerIndex;
            SteamVR_Controller.Input(controllerIndex).TriggerHapticPulse(2000);
        }
    }
    

    void PadClick(object sender, ClickedEventArgs e)
    {
        if (heldObject != null)
        {
            //Drop held object
            DropObject();
            return;
        }

        if (touchedObject != null)
        {
            PickUpObject(touchedObject);
            return;
        }

        // Teleport
        float refY = teleportReference.position.y;
        Plane plane = new Plane(Vector3.up, -refY);
        Ray ray = new Ray(this.transform.position, transform.forward);

        bool hasGroundTarget = false;
        float dist = 0f;
        hasGroundTarget = plane.Raycast(ray, out dist);
        if (hasGroundTarget)
        {
            Vector3 newPos = ray.origin + ray.direction * dist - new Vector3(teleportReference.GetChild(0).localPosition.x, 0f, teleportReference.GetChild(0).localPosition.z);
            teleportReference.position = newPos;
        }
    }

    void TriggerClick(object sender, ClickedEventArgs e)
    {
        //Try to activate held object
        if (heldObject != null)
        {
            heldObject.GetComponent<WeaponController>().TriggerClick();
        }
    }

    void MenuClick(object sender, ClickedEventArgs e)
    {

    }

    void OnTriggerEnter(Collider other)
    {
        //Ignore held object
        if (other == heldObject) return;

        //Trigger is meleeweapon
        if (other.gameObject.CompareTag("MeleeWeapon") || other.gameObject.CompareTag("RangedWeapon"))
        {
            touchedObject = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Ignore held object
        if (other == heldObject) return;

        //Trigger is meleeweapon
        if (other.gameObject.CompareTag("MeleeWeapon") || other.gameObject.CompareTag("RangedWeapon"))
        {
            touchedObject = null;
        }
    }

    void PickUpObject(GameObject other)
    {
        Debug.Log("Picking up " + other.name);
        touchedObject = null;
        heldObject = other;
        heldObject.GetComponent<Rigidbody>().useGravity = false;
        //Deactivate collider for controller
        GetComponent<CapsuleCollider>().enabled = false;
        heldObject.GetComponent<CapsuleCollider>().isTrigger = true;
        //Align picked up object with controller
        other.transform.parent = gameObject.transform;
        other.transform.localPosition = new Vector3(0, 0, 0);
        other.transform.localRotation = Quaternion.identity;
        if (other.gameObject.CompareTag("MeleeWeapon"))
        {
            other.transform.localPosition = new Vector3(0, 0, 0);
            other.transform.localRotation = Quaternion.Euler(0f, 180f, 180f);
        }
        if (other.gameObject.CompareTag("RangedWeapon"))
        {
            other.transform.localPosition = new Vector3(0, -0.13f, -0.025f);
            other.transform.localRotation = Quaternion.Euler(0f, 90f, 70f);
        }
        
    }

    void DropObject()
    {
        heldObject.transform.parent = null;
        heldObject.GetComponent<Rigidbody>().useGravity = true;
        //PROBLEM if the weapon is partly in the floor gravity will not be deactivated properly and it will fall through
        //Reinstate collider for controller
        GetComponent<CapsuleCollider>().enabled = true;
        heldObject.GetComponent<CapsuleCollider>().isTrigger = false;

        heldObject = null;
        
    }

}
