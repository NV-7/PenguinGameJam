using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private SpriteRenderer spriteRenderer;

    public float health;
    private float maxHealth = 100f;

    public float healthBarWidth = 0.25f;
    public Transform healthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        getDirection();
        pointToMouse();

        if (health <= 0)
            {
                Destroy(gameObject);
                SceneManager.LoadScene("GameOver");
            }
        healthBar.localScale = new Vector3(health / 100f * healthBarWidth, healthBar.localScale.y, healthBar.localScale.z);
    }

    
    private void FixedUpdate()
    {
        movePlayer();
    }

    void movePlayer()
    {
        rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);

    }

    void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                health -= 50f;
            }
        }
    void getDirection()
    {
        float horizontalDirection = 0f;
        float verticalDirection = 0f;
        if (Keyboard.current.wKey.isPressed)
        {
            verticalDirection = 1;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            verticalDirection = -1;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            horizontalDirection = -1;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            horizontalDirection = 1;
        }

        moveDirection = new Vector2(horizontalDirection, verticalDirection).normalized;
    }


    private void pointToMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 135));
    }

    void LateUpdate() //updates after the enemy has moved, 
    {
        //keeps health bar static
        healthBar.rotation = Quaternion.identity;
        healthBar.position = transform.position + new Vector3(0, -0.9f, 0);
    }
}
