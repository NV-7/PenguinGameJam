using UnityEngine;
using UnityEngine.InputSystem;

public class GunTipController : MonoBehaviour
{
    public bool isDashing;

    public GameObject bulletPrefab;
    public AudioSource shootSound;
    private float sinceLastShot;
    public float shotDelay;

    private bool mustReleaseAfterDash;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shootSound = GetComponent<AudioSource>();
        sinceLastShot = 1000f;
        mustReleaseAfterDash = false;

    }

    // Update is called once per frame
    void Update()
    {

        isDashing = PlayerMovement.Instance != null && PlayerMovement.Instance.isDashing;
        sinceLastShot += Time.deltaTime;

        // If player is dashing, force a mouse release before shoting again
        if (isDashing)
        {
            mustReleaseAfterDash = true;
            return;
        }
        // Once mouse is released, allow shooting agiain
        if (mustReleaseAfterDash)
        {
            if (!Mouse.current.leftButton.wasPressedThisFrame)
            {
                mustReleaseAfterDash = false;
            }
            else
            {
                return;
            }
        }
        if (Mouse.current.leftButton.wasPressedThisFrame && !isDashing && sinceLastShot >= shotDelay)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            shootSound.Play();
            sinceLastShot = 0f;
        }
    }
}
