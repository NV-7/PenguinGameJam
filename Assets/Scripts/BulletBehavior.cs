using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BulletBehavior : MonoBehaviour
{
    public float velocity;
    public float timeAlive;
    public float maxTime;
    private Vector3 moveDirection;
    public AudioSource shootSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeAlive = 0f;
        maxTime = 2f;
        moveDirection = transform.up;
        shootSound = GetComponent<AudioSource>();
        shootSound.Play();
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
        transform.position += moveDirection * velocity * Time.deltaTime;
    }


}
