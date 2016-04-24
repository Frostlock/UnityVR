using UnityEngine;
using System.Collections;

public class RoomController : MonoBehaviour{

    public int maxRoomWidth;
    public int minRoomWidth;
    private int roomWidth;

    // Use this for initialization
    void Start() {
        SetRoomWidth(maxRoomWidth);
    }

    public int GetRoomWidth()
    {
        return roomWidth;
    }

    public void SetRoomWidth(int newRoomWidth) {
        roomWidth = newRoomWidth;
        //Make sure roomWidth stays within bounds
        if (roomWidth > maxRoomWidth) roomWidth = maxRoomWidth;
        if (roomWidth < minRoomWidth) roomWidth = minRoomWidth;
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

    
    /*
     * Mouse scroll to control roomsize
     *
    void Update () {
        //Debug.Log(Input.mouseScrollDelta);
        int newRoomWidth = newRoomWidth = roomWidth + 2 * (int) Input.mouseScrollDelta[1];
        SetRoomWidth(newRoomWidth);
    }
    */
}
