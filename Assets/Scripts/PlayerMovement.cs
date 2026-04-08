using System;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public float dashForce;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Vector2 dashDirection;
    private SpriteRenderer spriteRenderer;
    private Boolean dashRequest = false;
<<<<<<< Updated upstream
    public GameObject damageFlash;
    public float knockbackAmount;
=======
    public GameObject ghost;
>>>>>>> Stashed changes
   

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
        damageFlash.SetActive(false);
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

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if(moveDirection != Vector2.zero)
            {
                dashDirection = moveDirection;
            }
            else
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                dashDirection = (Vector2)(mousePos - transform.position).normalized;
            }
                dashRequest = true;
                Debug.Log("Request Dash");
        }

    }

    
    private void FixedUpdate()
    {
       

        if (dashRequest)
        {
            dash();
            dashRequest = false;
            Debug.Log("Dashing, Position " + this.transform.position );

            
        }
        else
        {
            movePlayer();
        }
    }

    void movePlayer()
    {
        rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);

    }

    void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                health -= 20f;
                doDamageFlash();
                doKnockback(collision);
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

    private void dash()
    {

        //rb.AddForce(dashDirection * dashForce, ForceMode2D.Impulse);
<<<<<<< Updated upstream
        //rb.linearVelocity = dashDirection * dashForce;
        rb.MovePosition(rb.position + dashDirection * dashForce * speed * Time.fixedDeltaTime);

=======
        Vector3 pastPos = this.transform.position;
        rb.linearVelocity = dashDirection * dashForce;
        spawnGhost(pastPos);
>>>>>>> Stashed changes
        Debug.Log("Initiate Dash");
        
    }

    private void spawnGhost(Vector3 pastPos)
    {
        Vector3 direction = this.transform.position - pastPos;
        int numOfGhosts = 5;
        for(int i = 0; i < numOfGhosts; i++)
        {
            Instantiate(ghost, direction/(numOfGhosts - i + 1), Quaternion.identity);
        }
        
    }

    void LateUpdate() //updates after the enemy has moved, 
    {
        //keeps health bar static
        healthBar.rotation = Quaternion.identity;
        healthBar.position = transform.position + new Vector3(0, -0.9f, 0);
    }

    void doDamageFlash()
    {
        damageFlash.SetActive(true);
        Invoke("hideDamageFlash", 0.2f);
        
    }

     void hideDamageFlash()
    {
        damageFlash.SetActive(false);
    }

    void doKnockback(Collision2D collision)
    {
        float time = 0f;
        float maxTime = 0.2f;
        while(time < maxTime)
        {
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            rb.MovePosition(rb.position + knockbackDirection * knockbackAmount * Time.fixedDeltaTime);
            time += Time.deltaTime;
        }
      }
}
