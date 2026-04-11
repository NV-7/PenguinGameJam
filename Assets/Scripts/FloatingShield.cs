using UnityEngine;

public class FloatingShield : MonoBehaviour
{
    public float radius = 1.5f;
    public float rotationSpeed = 120f;
    public float damage = 10f;

    private Transform player;
    private float angle = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerMovement.Instance != null)
        {
            player = PlayerMovement.Instance.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }

        angle += rotationSpeed * Time.unscaledDeltaTime;
        float radians = angle * Mathf.Deg2Rad;

        Vector3 offset = new Vector3(
            Mathf.Cos(radians),
            Mathf.Sin(radians), 0f)
             * radius;
        transform.position = player.position + offset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.health -= damage;
            }
        }
    }
}
