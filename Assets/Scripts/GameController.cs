using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerInputActions playerControls;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingsPanel;
    private InputAction escape;
    // Start is called before the first frame update
    void Awake()
    {
        playerControls = new PlayerInputActions();
        Time.timeScale = 1;
    }

    private void OnEnable()
    {
        escape = playerControls.Player.Escape;
        escape.Enable();
        escape.performed += OpenClosePause;
    }

    private void OnDisable()
    {
        escape.Disable();
    }

    private void OpenClosePause(InputAction.CallbackContext context)
    {
        if (pausePanel.activeInHierarchy == true)
            pausePanel.SetActive(false);
        else
            pausePanel.SetActive(true);
        if (Time.timeScale == 1)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void ClosePause()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void OpenOptions()
    {
        settingsPanel.SetActive(!settingsPanel.activeInHierarchy);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(2);
    }
}
