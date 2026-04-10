using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShield : MonoBehaviour
{
    public float shieldRadius = 5f;
    public float shieldOrbitSpeed = 90f;

    private float shieldAngle = 53f;
    private Transform playerTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setPlayerTransform(GameObject.FindGameObjectWithTag("Player").transform);
    }

    // Update is called once per frame
    void Update()
    {
        ShieldMovement();
    }   


    private void FixedUpdate()
    {
        
    }

    private void ShieldMovement()
    {
        shieldAngle += shieldOrbitSpeed * Time.deltaTime;
        float radian = shieldAngle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0) * shieldRadius;
        transform.position = playerTransform.position + offset;
        transform.rotation = Quaternion.Euler(0, 0, shieldAngle - 90f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if(player == null||player.shield!= null)
        {
            return;
        }

        player.shield = gameObject;
        setPlayerTransform(player.transform);

        GetComponent<Collider2D>().enabled = false;
    }

    public void setPlayerTransform(Transform player)
    {
        playerTransform = player;
    }


}
