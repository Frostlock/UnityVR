using UnityEngine;
using System.Collections;

public class ChunkController : MonoBehaviour {

    private int size = 5; // Don't set this too high :)
    public GameObject blockPrefab;

    private GameObject[,,] blockArray;
    
    // Use this for initialization
    void Start () {
        blockArray = new GameObject[size,size,size];
        GameObject block;
        Vector3 position;
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                for (int z = 0; z < size; z++)
                {
                    position = new Vector3(transform.position.x + x,
                                   transform.position.y + y,
                                   transform.position.z + z);
                    block = (GameObject) Instantiate(blockPrefab, position, transform.rotation);
                    block.transform.parent = transform;
                    block.name = "Block (" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ")";
                    blockArray[x, y, z] = block;
                }
            }
        }
    }
	
    // Init Full

    // Init Random

	// Update is called once per frame
	void Update () {
	
	}
}
