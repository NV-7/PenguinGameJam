using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float gameTime;
    public bool gameActive;
    public GameObject PausePanel;
    public GameObject SettingsPanel;

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

    void Start()
    {
        gameActive = true;
    }

    void Update()
    {
        if (gameActive)
        {
            gameTime += Time.deltaTime;
            UIController.Instance.UpdateTimer(gameTime);
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                Pause();
            }
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
    public void Pause()
    {
        if (PlayerMovement.Instance.pausePanel.activeSelf == false &&
            PlayerMovement.Instance.gameOverScreen.activeSelf == false)
        {
            PlayerMovement.Instance.pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            PlayerMovement.Instance.pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void openSettingsMenu()
    {
        SettingsPanel.SetActive(true);
        PausePanel.SetActive(false);

    }
    public void closeSettingsPanel()
    {
        SettingsPanel.SetActive(false);
        PausePanel.SetActive(true);

    }
}
