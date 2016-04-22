using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

    public GameObject hazard;
    public Vector3 spawnValues;
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
            for (int i = 0; i < hazardCount; i++)
            {
                spawnHazard();
                yield return new WaitForSeconds(spawnWait);
            }
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
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x),
                                            Random.Range(1, spawnValues.y),
                                            Random.Range(-spawnValues.z, spawnValues.z));
        Quaternion spawnRotation = Quaternion.identity;
        float s = Random.Range(0.5f, 2.5f);
        Vector3 spawnScale = new Vector3(s, s, s);
                                            
        GameObject newHazard = (GameObject) Instantiate(hazard, spawnPosition, spawnRotation);
        newHazard.transform.localScale = spawnScale;
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
