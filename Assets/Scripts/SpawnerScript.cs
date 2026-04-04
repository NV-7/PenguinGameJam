using UnityEngine;

public class SpawnerScript : MonoBehaviour
{

    public int maxNumEnemy = 5;
    public int enenmyCounter = 0;
    public GameObject Enemy1;
    public float TimeOffset = 2f;
    private float TimePassed = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        TimePassed += Time.deltaTime;
        

        if (enenmyCounter < maxNumEnemy)
        {

            if(TimePassed >= TimeOffset)
            {
            spawnEnemy();
            enenmyCounter++;
            TimePassed = 0f;
            }
        }
        
     
            
    }

    void spawnEnemy()       
    {
        Instantiate(Enemy1, this.transform.position, Quaternion.identity);

    }

}
