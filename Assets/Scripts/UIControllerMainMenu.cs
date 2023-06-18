using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIControllerMainMenu : MonoBehaviour
{
    public static UIControllerMainMenu Instance;

    [SerializeField] private GameObject panelTitle;   
    [SerializeField] private GameObject panelSettings;   
    [SerializeField] private GameObject panelCredits;
    private GameObject fade;

    private float secondsToExit = 3f;

    private void Awake()
    {
        Instance = this;
        fade = GameObject.Find("FadeIn");
        Invoke("DestroyFade", 2f);
    }

    private void DestroyFade()
    {
        Destroy(fade.gameObject);
    }

    private void Start()
    {
        OpenPanelTitle();
    }

    public void Play()
    {
        // Start game
        Debug.Log("Play");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Exit()
    {
        StartCoroutine(WaitExit());
    }

    private IEnumerator WaitExit()
    {
        // Exit animation...

        yield return new WaitForSeconds(secondsToExit);


        Application.Quit();
    }


    public void OpenPanelTitle()
    {
        panelTitle.SetActive(true);
        panelSettings.SetActive(false);
        panelCredits.SetActive(false);
    }

    public void OpenPanelSettings()
    {
        panelTitle.SetActive(false);
        panelSettings.SetActive(true);
        panelCredits.SetActive(false);
    }

    public void OpenPanelCredits()
    {
        panelTitle.SetActive(false);
        panelSettings.SetActive(false);
        panelCredits.SetActive(true);
    }
}
