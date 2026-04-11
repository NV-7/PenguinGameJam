using UnityEngine;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    public GameObject levelUpPanel;
    public GameObject shieldPrefab;

    private GameObject currentShield;
    [SerializeField] TMP_Text shieldText;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateShieldText()
    {
        if (shieldText == null)
        {
            return;
        }

        if (currentShield == null)
        {
            shieldText.text = "Unlock Shield";
        }
        else
        {
            shieldText.text = "Upgrade Shield";
        }
    }

    public void UpgradeMaxHealth()
    {
        if (PlayerMovement.Instance != null)
        {
            PlayerMovement.Instance.maxHealth += 5f;
            PlayerMovement.Instance.health += 5f;

            if (PlayerMovement.Instance.health > PlayerMovement.Instance.maxHealth)
            {
                PlayerMovement.Instance.health = PlayerMovement.Instance.maxHealth;
            }
        }
        CloseLevelUp();
    }
    public void UpgradeWeaponDamage()
    {
        GunTipController gun = FindFirstObjectByType<GunTipController>();

        if (gun != null)
        {
            gun.bulletDamage = Mathf.Min(gun.bulletDamage + 5f, 50f);
        }

        CloseLevelUp();
    }

    public void UpgradeFireRate()
    {
        GunTipController gun = FindFirstObjectByType<GunTipController>();
        if (gun != null)
        {
            gun.shotDelay = Mathf.Max(0.1f, gun.shotDelay - 0.05f);
        }
        CloseLevelUp();
    }

    public void UpgradeFloatingShield()
    {
        if (PlayerMovement.Instance != null && shieldPrefab != null)
        {
            if (currentShield == null)
            {
                currentShield = Instantiate(shieldPrefab);
                currentShield.transform.SetParent(PlayerMovement.Instance.transform);
                currentShield.transform.localPosition = Vector3.zero;

                FloatingShield shield = currentShield.GetComponent<FloatingShield>();
                if (shield != null)
                {
                    shield.damage = 10f;
                    shield.rotationSpeed = 120f;
                    shield.radius = 1.2f;
                }
            }
            else
            {
                FloatingShield shield = currentShield.GetComponent<FloatingShield>();
                if (shield != null)
                {
                    shield.damage += 5f;
                    shield.rotationSpeed += 20f;
                    shield.radius += 0.1f;
                }
            }
            updateShieldText();
            CloseLevelUp();
        }
    }


    void CloseLevelUp()
    {
        EXPController.Instance.CloseLevelUpScreen();
    }
}
