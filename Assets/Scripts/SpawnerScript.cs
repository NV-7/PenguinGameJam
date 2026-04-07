using UnityEngine;

public class SpawnerScript : MonoBehaviour
{

    public int maxNumEnemy = 5;
    public int enenmyCounter = 0;
    public GameObject Enemy1;
    public float TimeOffset = 2f;
    public Transform playerTransform;
    public float spawnRadius = 10f;
    public float spawnInterval = 2f; // Two second spawn interval
    private float TimePassed = 0f;
    private float timer = 0f; 

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       // Changess spawn rate as game continues, Max spawn rate is 0.2
        float interval = Mathf.Max(0.3f, spawnInterval - (Time.timeSinceLevelLoad / 60f));

        if (enenmyCounter < maxNumEnemy)
        {
            timer += Time.deltaTime;
            if (timer >= interval)
            {
                spawnEnemy();
                timer= 0f;
            }
        }
    }

    private void FixedUpdate()
    {
       
        
     
            
    }

    void spawnEnemy()       
    {
        // Vector2 inside a unit circle
        Vector2 unitDirection = Random.insideUnitCircle.normalized;

        Vector3 spawnPos = playerTransform.position + new Vector3(unitDirection.x, unitDirection.y, 0) * spawnRadius;

        Instantiate(Enemy1, spawnPos, Quaternion.identity);
        enenmyCounter++;

    }

    public void descrementEnemeyCount()
    {
        enenmyCounter--;
    }

}
