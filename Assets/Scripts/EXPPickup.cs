using UnityEngine;

public class EXPPickup : MonoBehaviour
{
    public int expAmount = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EXPController.Instance.AddEXP(expAmount);
            Destroy(gameObject);
        }
    }
}
