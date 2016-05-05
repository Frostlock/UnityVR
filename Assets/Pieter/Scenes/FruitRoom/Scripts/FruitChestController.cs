using UnityEngine;
using System.Collections;

public class FruitChestController : InteractionScript
{

    public bool active = false;
    public GameObject glowEffect;
    public float spawnRadius = 0f;
    public int maxWaveSize = 3;


    public GameObject apple;
    public GameObject apricot;
    public GameObject strawberry;
    public GameObject pear;
    public GameObject coconut;
    private GameObject[] fruitOptions;

    private IEnumerator spawnCoRoutine;
    private Animation chestAnimation;

    private float tumbleForce = 8f;
    private float upwardForce = 8f;

    void Start () {
        //look up available fruit options
        fruitOptions = new GameObject[5];
        fruitOptions[0] = apple;
        fruitOptions[1] = apricot;
        fruitOptions[2] = strawberry;
        fruitOptions[3] = pear;
        fruitOptions[4] = coconut;

        chestAnimation = gameObject.GetComponent<Animation>();

        if (active)
        {
            active = false;
            StartSpawning();
        }
    }

    public override void Interact()
    {
        if (active) StopSpawning();
        else StartSpawning();
    }

    void StartSpawning()
    {
        if (active) return;
        active = true;
        glowEffect.GetComponent<ParticleSystem>().Play();
        //Open chest animation
        chestAnimation.Play();
        chestAnimation["ChestAnim"].speed = 1;
        //Start spawning co routine
        spawnCoRoutine = spawnLoop();
        StartCoroutine(spawnCoRoutine);
    }

    void StopSpawning()
    {
        if (!active) return;
        active = false;
        glowEffect.GetComponent<ParticleSystem>().Pause();
        //Stop spawning co routine
        StopCoroutine(spawnCoRoutine);
        //Close chest
        chestAnimation.Play("ChestAnim");
        chestAnimation["ChestAnim"].speed = -1;
        //chestAnimation["ChestAnim"].time = 0; // Don't need this, this would immediately reset the animation to the first frame.
    }

    IEnumerator spawnLoop()
    {
        //Wait for chest open animation to complete
        yield return new WaitForSeconds(3);
        while (true)
        {
            int waveSize = Random.Range(1, maxWaveSize);
            for (int i = 0; i < waveSize; i++)
            {
                spawnFruit();
                float waitTime = Random.Range(0.2f, 1f);
                yield return new WaitForSeconds(waitTime);
            }
                

            //Wait for fruit to be destroyed
            int remainingHazards = GameObject.FindGameObjectsWithTag("Fruit").Length;
            while (remainingHazards > 0)
            {
                yield return new WaitForSeconds(1);
                remainingHazards = GameObject.FindGameObjectsWithTag("Fruit").Length;
            }

            //Pause before starting next wave
            yield return new WaitForSeconds(1);

        }
    }

    void spawnFruit()
    {
        GameObject fruit = fruitOptions[Random.Range(0, fruitOptions.Length)];

        //Spawn a new fruit
        Vector3 spawnCenter = gameObject.transform.position;
        Vector2 spawnOffSet = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPosition = new Vector3(spawnCenter.x + spawnOffSet.x,
                                            spawnCenter.y,
                                            spawnCenter.z + spawnOffSet.y);
        Quaternion spawnRotation = Quaternion.identity;
        GameObject newFruit = (GameObject)Instantiate(fruit, spawnPosition, spawnRotation);

        //Set an angular velocity to make the fruit spin
        Rigidbody rb = newFruit.GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * tumbleForce;
              
        //Apply a force to throw the fruit up
        float x, y, z;
        x = Random.Range(-0.15f, 0.15f);
        y = 0.75f; // Random.Range(0.5f, 1f);
        z = Random.Range(-0.01f, 0.01f);
        Vector3 direction = new Vector3 (x, y, z);
        rb.AddForce(direction * upwardForce, ForceMode.Impulse); ;
    }
}
