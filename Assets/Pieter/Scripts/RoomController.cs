using UnityEngine;
using System.Collections;

public class RoomController : MonoBehaviour{

    public int roomWidth;

    // Use this for initialization
    void Start() {
        SetRoomWidth();
    }

    void SetRoomWidth() { 
        //Make sure roomWidth stays within bounds
        if (roomWidth > 200) roomWidth = 200;
        if (roomWidth < 10) roomWidth = 10;
        //Move walls out to create room of roomWidth
        int translation = roomWidth / 2;
        foreach (Transform t in transform) {
            if (t.name == "WallPosZ") {
                t.transform.position = new Vector3(t.transform.position.x, t.transform.position.y, translation);
            }
            else if (t.name == "WallNegZ")
            {
                t.transform.position = new Vector3(t.transform.position.x, t.transform.position.y, -1 * translation);
            }
            else if (t.name == "WallPosX")
            {
                t.transform.position = new Vector3(translation, t.transform.position.y, t.transform.position.z);
            }
            else if (t.name == "WallNegX")
            {
                t.transform.position = new Vector3(-1 * translation, t.transform.position.y, t.transform.position.z);
            }
            else if (t.name == "Ceiling")
            {
                t.transform.position = new Vector3(t.transform.position.x, roomWidth, t.transform.position.z);
            }
        }
	}

    // Update is called once per frame
    void Update () {
        //Debug.Log(Input.mouseScrollDelta);
        roomWidth = roomWidth + 2 * (int) Input.mouseScrollDelta[1];
        SetRoomWidth();


    }
}
