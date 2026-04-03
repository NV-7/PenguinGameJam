using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rb;
    private Vector2 moveDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        getDirection();
    }
        

    private void FixedUpdate()
    {
        movePlayer();
    }

    void movePlayer()
    {
        rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);

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
        if(Keyboard.current.aKey.isPressed)
        {
            horizontalDirection = -1;
        }
        if(Keyboard.current.dKey.isPressed)
        {
            horizontalDirection = 1;
        }

        moveDirection = new Vector2(horizontalDirection, verticalDirection).normalized;
    }

}
