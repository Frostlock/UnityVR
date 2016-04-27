using UnityEngine;
using System.Collections;
using System.IO;

public class SpawnSomeStuff : MonoBehaviour {

    public GameObject drivePreFab;
    public Transform driveSpawnLocation;
    public Vector3 driveSpawnOffset;

    // Use this for initialization
    void Start () {
        Debug.Log("Spawning stuff");

        string[] drives = Directory.GetLogicalDrives();
        int offSetCount = 0;
        foreach (string drive in drives)
        {
            Debug.Log(drive);

            //This works but not for my E drive (empty DVD drive).
            string[] driveSubFolders = Directory.GetDirectories(drive);
            Debug.Log(driveSubFolders[0]);
            
            //Spawn object to represent drive
            GameObject driveObj = (GameObject) Instantiate(drivePreFab, driveSpawnLocation.position, driveSpawnLocation.rotation);
            TextMesh driveLabel = driveObj.GetComponentInChildren<TextMesh>();
            driveLabel.text = drive;

            driveObj.transform.position = new Vector3(driveObj.transform.position.x + offSetCount * driveSpawnOffset.x,
                                                      driveObj.transform.position.y + offSetCount * driveSpawnOffset.y, 
                                                      driveObj.transform.position.z + offSetCount * driveSpawnOffset.z);
            offSetCount = offSetCount + 1;
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
