using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Transform player;
    public float offsetX;
    public float offsetY;
    public float cameraMoveSpeed = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        if (player != null)
        {
            // Camera position based on player and offset
            transform.position = new Vector3(player.position.x + offsetX, player.position.y + offsetY, -10f);

            // Smoothly move camera towards the target position
            //transform.position = Vector3.Lerp(transform.position, targetPos,
            //cameraMoveSpeed * Time.deltaTime);
        }
    }
}
