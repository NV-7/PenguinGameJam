using UnityEngine;

public class GhostingEffect : MonoBehaviour
{
    public float fadeSpeed = 0.3f;
    private SpriteRenderer sprite;
    private Color color;
    private float timePassed = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        color = sprite.color;
    }

    // Update is called once per frame
    void Update()
    {
        color.a -= fadeSpeed * Time.deltaTime;
        sprite.color = color;

        if(color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
