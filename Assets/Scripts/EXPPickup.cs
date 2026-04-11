using UnityEngine;

public class EXPPickup : MonoBehaviour
{
    public int expAmount = 1;
    public AudioClip pickupSound;
    private bool pickedUp = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!pickedUp && collision.CompareTag("Player"))
        {
            pickedUp = true;
            if (EXPController.Instance != null)
            {
                EXPController.Instance.AddEXP(expAmount);
            }

            if (pickedUp != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }
            Destroy(gameObject);

        }
    }
}
