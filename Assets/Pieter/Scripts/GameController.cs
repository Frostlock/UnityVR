using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

    public GameObject room;
    public GameObject hazard;
    //public Vector3 spawnValues;
    private int spawnBoundary;

    public int hazardCount;
    public float startWait;
    public float spawnWait;
    public float waveWait;

    //public GUIText scoreText;
    //public GUIText restartText;
    //public GUIText gameOverText;

    private int score;
    private bool gameOver;
    private bool restart;

    void Start()
    {
        score = 0;
        gameOver = false;
        restart = false;
        //restartText.text = "";
        //gameOverText.text = "";

        //updateScore();
        StartCoroutine(spawnWaves());
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    IEnumerator spawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            //Start wave by shrinking room
            RoomController rc = room.GetComponent<RoomController>();
            rc.SetRoomWidth(rc.GetRoomWidth() - 3);

            //Calculate spawn boundaries
            int roomWidth = room.GetComponent<RoomController>().GetRoomWidth();
            spawnBoundary = roomWidth / 2 - 3;

            //Spawn hazards for the wave
            for (int i = 0; i < hazardCount; i++)
            {
                spawnHazard();
                //Pause before spawning next hazard
                yield return new WaitForSeconds(spawnWait);
            }

            //Wait for all hazards to be destroyed
            int remainingHazards = GameObject.FindGameObjectsWithTag("Hazard").Length;
            while (remainingHazards > 0)
            {
                yield return new WaitForSeconds(1);
                remainingHazards = GameObject.FindGameObjectsWithTag("Hazard").Length;
            }

            //Pause before starting next wave
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                //restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
    }

    void spawnHazard()
    {
        //Spawn a new hazard
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnBoundary, spawnBoundary),
                                            Random.Range(1, spawnBoundary),
                                            Random.Range(-spawnBoundary, spawnBoundary));
        Quaternion spawnRotation = Quaternion.identity;                                           
        GameObject newHazard = (GameObject) Instantiate(hazard, spawnPosition, spawnRotation);

        //Apply some force to make the hazard move
        Rigidbody rb = newHazard.GetComponent<Rigidbody>();
        int force = 1;
        //Random direction
        rb.AddForce(Random.onUnitSphere * force, ForceMode.Impulse);
        //Toward the center of the scene
        force = 30;
        Vector3 normPos = newHazard.transform.position.normalized;
        Vector3 toCenter = new Vector3(-normPos.x, -normPos.y, -normPos.z);
        rb.AddForce(toCenter * force, ForceMode.Impulse);
    }

    public void addScore(int newScoreValue)
    {
        score += newScoreValue;
        updateScore();
    }

    void updateScore()
    {
        //scoreText.text = "Score: " + score;
    }

    public void endGame()
    {
        //gameOverText.text = "Game Over";
        gameOver = true;
    }
}
