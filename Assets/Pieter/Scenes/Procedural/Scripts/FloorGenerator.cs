using UnityEngine;
using System.Collections;

public class FloorGenerator : MonoBehaviour {

    public GameObject tilePrefab;
    public int numberOfTiles = 1;
    private Vector2 origin = new Vector2(0f, 0f);
    private float floorLevel = 0f;

	// Use this for initialization
	void Start () {
        Vector3 position;
        GameObject tileObj;
        //Place first tile at origin
        position = new Vector3(origin.x, floorLevel, origin.y);
        tileObj = (GameObject) Instantiate(tilePrefab, position, transform.rotation);
        tileObj.transform.parent = gameObject.transform;

        //Place additional tiles
        for (int i = 1; i < numberOfTiles; i++)
        {
            //Currently just add in the X direction
            position = new Vector3(origin.x + i, floorLevel, origin.y);
            tileObj = (GameObject)Instantiate(tilePrefab, position, transform.rotation);
            tileObj.transform.parent = gameObject.transform;
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
