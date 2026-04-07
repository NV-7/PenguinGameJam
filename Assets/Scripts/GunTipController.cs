using UnityEngine;
using UnityEngine.InputSystem;

public class GunTipController : MonoBehaviour
{
    public bool canShoot;
    public GameObject bulletPrefab;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canShoot = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Mouse.current.leftButton.isPressed)
        {
            if (canShoot)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                canShoot = false;
                
            }
            }
            if(!(Mouse.current.leftButton.isPressed))
            {
                canShoot = true;
            }

    }

        
}
