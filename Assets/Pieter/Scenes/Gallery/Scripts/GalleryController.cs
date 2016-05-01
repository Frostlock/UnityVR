using UnityEngine;
using System.Collections;
using System.IO;

public class GalleryController : MonoBehaviour {

    public GameObject picturePrefab;
    public Vector3 spawnOffset;
    public bool includeWidthInOffset = true;
    public bool includeHeightInOffset = false;

    private string pathPrefix = @"file://";
    public string picturePath = @"F:\Revive\Tracer\";

    public float pictureScaling = 0.001f;
    // Use this for initialization
    void Start () {
        StartCoroutine(loadImages());
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator loadImages()
    {
        //GameObject pictureObj = (GameObject)Instantiate(picturePrefab, transform.position, transform.rotation);
        string[] files = Directory.GetFiles(picturePath);
        WWW webFile;
        Texture2D texTmp;
        GameObject pictureObj;
        Vector3 offset, position, scale, min, max;
        float width, height, depth;
        offset = new Vector3(0f, 0f, 0f);
        for (int i = 0; i < files.Length; i++)
        {
            //Debug.Log("Loading " + files[i]);
            webFile = new WWW(pathPrefix + files[i]);
            yield return webFile;
            texTmp = webFile.texture;

            //Instantiate a picture object
            position = new Vector3(transform.position.x + offset.x,
                                   transform.position.y + offset.y,
                                   transform.position.z + offset.z);
            pictureObj = (GameObject)Instantiate(picturePrefab, position, transform.rotation);
            pictureObj.transform.parent = gameObject.transform;
            
            //Apply the texture to the gameobject
            pictureObj.GetComponent<Renderer>().material.mainTexture = texTmp;

            //Scale the image properly
            scale = new Vector3(pictureObj.transform.localScale.x * texTmp.width * pictureScaling,
                                pictureObj.transform.localScale.y * texTmp.height * pictureScaling,
                                pictureObj.transform.localScale.z);
            pictureObj.transform.localScale = scale;

            //Determine width and height in worldspace
            min = pictureObj.GetComponent<Renderer>().bounds.min;
            max = pictureObj.GetComponent<Renderer>().bounds.max;
            width = max.x - min.x;
            height = max.y - min.y;
            depth = max.z - min.z;

            //refine position (to enable spacing out the images)
            position = new Vector3 (pictureObj.transform.position.x + width / 2,
                                    pictureObj.transform.position.y + height/ 2,
                                    pictureObj.transform.position.z + depth/ 2);
            pictureObj.transform.position = position;
            
            offset.x = offset.x + spawnOffset.x;
            if (includeWidthInOffset) offset.x = offset.x + width;
            offset.y = offset.y + spawnOffset.y;
            if (includeHeightInOffset) offset.y = offset.y + height;
            offset.z = offset.z + spawnOffset.z;

        }
    }
}
