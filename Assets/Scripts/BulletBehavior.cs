using Unity.VisualScripting;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float velocity;
    public float timeAlive;
    public float maxTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeAlive = 0f;
        velocity = 6f;
        maxTime = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;
        if(timeAlive >= maxTime)
        {
            Destroy(gameObject);
        }
        //transform.rotation = GameObject.FindGameObjectWithTag("Player").transform.rotation;
        transform.position += new Vector3(velocity * Time.deltaTime, 0, 0);
    }
}
