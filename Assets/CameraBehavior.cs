using Mono.Cecil;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Transform player;
    public float offsetX;
    public float offsetY;
    public float cameraMoveSpeed = 0.3f;
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
            Vector3 newPos = new Vector3(player.position.x + offsetX, player.position.y + offsetY, -10);

            this.transform.position = Vector3.Lerp(this.transform.position, newPos, cameraMoveSpeed * Time.deltaTime);
        }
    }
}
