using System;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VectorGraphics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    public float speed;
    public float dashForce;
    public float dashDuration = 0.2f;
    private float dashTimer = 0f;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Vector2 dashDirection;
    private SpriteRenderer spriteRenderer;
    private Boolean dashRequest = false;

    public bool isDashing = false;

    public GameObject damageFlash;
    public float knockbackAmount;

    public GameObject ghost;

    public AudioClip dashSound;
    private AudioSource audioSource;
    public int ghostCount = 5;
    //public float ghostInterval = 0.02f;
    public float ghostSpawnDistance = 0.5f;
    public GameObject gameOverScreen;
    public GameObject pausePanel;
    public GameObject shield;


    public float health;
    private float maxHealth = 100f;

    public float healthBarWidth = 0.25f;
    public Transform healthBar;


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        audioSource = this.GetComponent<AudioSource>();

        health = maxHealth;
        damageFlash.SetActive(false);
        //shield = transform.transform.GetChild(2).gameObject;
        //shield.GetComponent<Rigidbody2D>().freezeRotation = true;


    }

    // Update is called once per frame
    void Update()
    {
        getDirection();
        pointToMouse();


        if (health <= 0)
        {
            GameManager.Instance.gameActive = false;
            Destroy(gameObject);
            gameOverScreen.SetActive(true);
        }
        healthBar.localScale = new Vector3(health / 100f * healthBarWidth, healthBar.localScale.y, healthBar.localScale.z);

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                isDashing = false;
            }
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (moveDirection != Vector2.zero)
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
            Debug.Log("Dashing, Position " + this.transform.position);
            audioSource.PlayOneShot(dashSound);

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
        // if (shield != null)
        // {
        //     shield.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 135));
        // }

    }

    public void dash()
    {

        //rb.AddForce(dashDirection * dashForce, ForceMode2D.Impulse);
        //rb.linearVelocity = dashDirection * dashForce;

        isDashing = true;
        dashTimer = dashDuration;
        rb.MovePosition(rb.position + dashDirection * dashForce * speed * Time.fixedDeltaTime);


        Vector3 pastPos = this.transform.position;
        rb.linearVelocity = dashDirection * dashForce;
        StartCoroutine(SpawnGhost());

        Debug.Log("Initiate Dash");

    }

    private System.Collections.IEnumerator SpawnGhost()
    {

        int counter = 0;
        Vector3 prevPos = transform.position;
        while (counter < ghostCount)
        {
            float travelDistance = Vector3.Distance(prevPos, transform.position);

            if (travelDistance >= ghostSpawnDistance)
            {
                GameObject ghosts = Instantiate(ghost, transform.position, transform.rotation);
                SpriteRenderer ghostSprite = ghosts.GetComponent<SpriteRenderer>();
                ghostSprite.sprite = spriteRenderer.sprite;
                ghostSprite.flipX = spriteRenderer.flipX;
                ghostSprite.flipY = spriteRenderer.flipY;

                prevPos = transform.position;
                counter++;

            }
            yield return null;
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
        while (time < maxTime)
        {
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            rb.MovePosition(rb.position + knockbackDirection * knockbackAmount * Time.fixedDeltaTime);
            time += Time.deltaTime;
        }
    }


}
