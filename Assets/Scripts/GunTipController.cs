using UnityEngine;
using UnityEngine.InputSystem;

public class GunTipController : MonoBehaviour
{
    public bool canShoot;
    public GameObject bulletPrefab;
    public AudioSource shootSound;
    private float sinceLastShot;
    public float shotDelay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canShoot = true;
        shootSound = GetComponent<AudioSource>();
        sinceLastShot = 1000f;
        
    }

    // Update is called once per frame
    void Update()
    {
        sinceLastShot += Time.deltaTime;
        if(Mouse.current.leftButton.isPressed)
        {
            if (canShoot)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                canShoot = false;
                shootSound.Play();
                sinceLastShot = 0f;
            }
            }

            if(!(Mouse.current.leftButton.isPressed) && sinceLastShot >= shotDelay)
            {
                canShoot = true;
            }

    }

        
}
