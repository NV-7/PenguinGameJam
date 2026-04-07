using UnityEngine;
using UnityEngine.Rendering;

/*
resources
https://www.youtube.com/watch?v=RCOxhTsbAWo&list=PLgXA5L5ma2Bveih0btJV58REE2mzfQLOQ&index=9
https://discussions.unity.com/t/lookat-2d-equivalent/88118
*/
public class EnemyController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Transform player;
    public float moveSpeed;
    
    public float health;
    private float maxHealth = 100f;
    public float bulletDamage;
    public float healthBarWidth = 0.25f;
    public Transform healthBar;
    public GameObject spawner;
    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        spawner = GameObject.FindGameObjectWithTag("Spawner");
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
            {
            spawner.GetComponent<SpawnerScript>().descrementEnemeyCount();
            Destroy(gameObject);
            }

        try
        {
            Vector3 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 135));
            
        }
        catch
        {
            //player is dead, do nothing
        }
        
        healthBar.localScale = new Vector3(health / 100f * healthBarWidth, healthBar.localScale.y, healthBar.localScale.z);
    }

    void FixedUpdate()
    {
        try{
        rb.MovePosition (Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime));
        }
        catch
        {
            
        }
    }
//player must have rigidbody (dynamic) and circle collider
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
                {
                    health -= bulletDamage;
                    Destroy(collision.gameObject);
                }
    }

    void LateUpdate() //updates after the enemy has moved, 
    {
        //keeps health bar static
        healthBar.rotation = Quaternion.identity;
        healthBar.position = transform.position + new Vector3(0, 0.7f, 0);
    }
}
