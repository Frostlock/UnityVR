using UnityEngine;
using System.Collections;

public class SwordController : MonoBehaviour {

    enum State
    {
        ready,
        retreat,
        slash
    }
    private State previousActionState = State.ready;
    private State actionState = State.ready;

    public float swordSpeed = 25f;
    public float swordRange = 1f;
    public float groundSpeed = 2f;
    public GameObject owner;
    public GameObject target;

    public Transform tip;
    public Transform guard;

    private Rigidbody rb;
    private Vector3 restPosition;
    private Quaternion restRotation;

    // Use this for initialization
    void Start () {
        rb = transform.GetComponentInChildren<Rigidbody>();
        restPosition = transform.localPosition; //new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        Debug.Log(restPosition);
        restRotation = transform.localRotation; //new Vector3(transform.eulerAngles.x , transform.eulerAngles.y, transform.eulerAngles.z);
        Debug.Log(restRotation);
	}
	
	// Update is called once per frame
	void Update () {
        //Move owner in swordrange
        float distanceToTarget = (target.transform.position - owner.transform.position).magnitude;
        if (distanceToTarget>swordRange)
        {
            owner.transform.position = Vector3.MoveTowards(owner.transform.position, target.transform.position, groundSpeed * Time.deltaTime);
        }
        else
        {
            if (previousActionState != actionState)
            {
                Debug.Log("State: " + actionState.ToString());
                previousActionState = actionState;
            }
            switch (actionState)
            {
                case State.ready:
                    Slash();
                    break;
                case State.retreat:
                    // move back to rest position
                    Vector3 pos = Vector3.MoveTowards(transform.localPosition, restPosition, swordSpeed * Time.deltaTime);
                    Quaternion rot = Quaternion.RotateTowards(transform.localRotation, restRotation, swordSpeed * 4 * Time.deltaTime);
                    int posDist, rotDist;
                    posDist = (int) (transform.localPosition - pos).magnitude;
                    Quaternion lr = transform.localRotation;
                    rotDist = (int) Quaternion.Dot(transform.localRotation, rot);
                    //TODO: rotDist == -1 might also happen?
                    if (posDist == 0 && rotDist == 1) actionState = State.ready;
                    transform.localPosition = pos;
                    transform.localRotation = rot;
                    break;
                case State.slash:
                    //Slash on-going, wait for it to complete
                    break;
                default:
                    Debug.LogError("Unknown sword state, don't know what to do?");
                    break;
            }
        }
        
        //TODO: Move sword down to be at a reasonable height of the target?





    }

    void Slash()
    {
        actionState = State.slash;
        //Where is the target?
        Vector3 direction = (target.transform.position - transform.position).normalized;

        /*
        //Point on unit circle
        Vector2 slasher = Random.insideUnitCircle;
        float moveForce = slasher.magnitude;
        */

        //Move Guard toward target
        rb.AddForceAtPosition(direction * swordSpeed / 4, guard.position);
        //Push tip toward target to create a slashing motion
        rb.AddForceAtPosition(direction * swordSpeed, tip.position);

    }

    void OnCollisionEnter(Collision col)
    {
        //return to rest position
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        actionState = State.retreat;

    }


}
