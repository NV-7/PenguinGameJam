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
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 135));
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }
}
