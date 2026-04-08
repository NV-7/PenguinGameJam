using UnityEngine;

public class GhostingEffect : MonoBehaviour
{
    public float ghostFading = 0.4f;
    private SpriteRenderer spriteRenderer;
    private float timePassed = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
       
        if(spriteRenderer != null)
        {
            
        }
    }
}
