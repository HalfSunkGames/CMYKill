using System;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    private List<GameObject> tutorialPanels;
    private int currentTutorial = 0;

    private void Start()
    {
        tutorialPanels = new List<GameObject>();

        foreach (Transform child in this.transform)
        {
            if (child.name != "Blocker")
            {
                GameObject obj = child.gameObject;
                tutorialPanels.Add(obj);
            }
        }

        Time.timeScale = 0;
        this.gameObject.SetActive(true);
    }

    public void NextTutorial()
    {
        try
        {
            tutorialPanels[currentTutorial].SetActive(false);
            currentTutorial++;
            tutorialPanels[currentTutorial].SetActive(true);
        }
        catch (Exception e)
        {
            Debug.Log("Error al pasar de tutorial");
        }

    }

    public void ExitTutorial()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }
}
