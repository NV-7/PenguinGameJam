using UnityEngine;

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

    public float healthBarWidth = 0.25f;
    public Transform healthBar;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
            {
                Destroy(gameObject);
            }

        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 135));
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

        healthBar.localScale = new Vector3(health / 100f * healthBarWidth, healthBar.localScale.y, healthBar.localScale.z);
    }


//player must have rigidbody (dynamic) and circle collider
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            health =- 10f;
        }
    }
}
