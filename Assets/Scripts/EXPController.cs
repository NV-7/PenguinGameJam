using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EXPController : MonoBehaviour
{
    public static EXPController Instance;

    public int level = 1;
    public GameObject levelUpPanel;
    public int currentEXP = 0;
    public int expToNextLevel = 10;

    [SerializeField] private Slider expSlider;
    [SerializeField] private TMP_Text expText;

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
        expSlider.maxValue = expToNextLevel;
        expSlider.value = currentEXP;
        UpdateUI();
    }

    public void AddEXP(int amount)
    {
        currentEXP += amount;

        if (currentEXP >= expToNextLevel)
        {
            currentEXP -= expToNextLevel;
            level++;

            expToNextLevel += 5;
            if (expSlider != null)
            {
                expSlider.maxValue = expToNextLevel;
            }
            ShowLevelUpScreen();
        }
        if (expSlider != null)
        {
            expSlider.value = currentEXP;
        }
        UpdateUI();
    }

    // Update is called once per frame
    void UpdateUI()
    {
        if (expText != null)
        {
            expText.text = currentEXP + " / " + expToNextLevel;
        }
    }

    void ShowLevelUpScreen()
    {
        if (levelUpPanel != null)
        {
            levelUpPanel.SetActive(true);
            Time.timeScale = 0f;

            UpgradeManager.Instance.updateShieldText();
        }
    }

    public void CloseLevelUpScreen()
    {
        if (levelUpPanel != null)
        {
            levelUpPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }


}
