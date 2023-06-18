using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreStatus : MonoBehaviour
{
    public static ScoreStatus Instance;

    [SerializeField] private TextMeshProUGUI timeTxt;
    [SerializeField] private TextMeshProUGUI killedTxt;
    [SerializeField] private TextMeshProUGUI rolledTxt;
    [SerializeField] private TextMeshProUGUI burnedTxt;
    [SerializeField] private GameObject recordTxt;
    public int killedEnemys;
    public int dicesRolled;
    public int dicesBurned;

    [SerializeField]private float timer;
    public float totalTime;
    [SerializeField]private string stringTime;

    private void Awake()
    {
        Instance = this;

        if (!PlayerPrefs.HasKey("totalTime"))
        {
            PlayerPrefs.SetInt("killedEnemys", 0);
            PlayerPrefs.SetInt("dicesRolled", 0);
            PlayerPrefs.SetInt("dicesBurned", 0);
            PlayerPrefs.SetFloat("totalTime", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time - timer;
        string minutes = Mathf.FloorToInt(t / 60).ToString("00");
        string seconds = Mathf.FloorToInt(t % 60).ToString("00");
        totalTime = t;
        stringTime = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void SaveScore()
    {

        bool record = false;

        if (PlayerPrefs.GetFloat("totalTime") < totalTime)
        {
            timeTxt.faceColor = Color.yellow; 
            record = true;
        }       
        if (PlayerPrefs.GetInt("killedEnemys") < killedEnemys)
        {
            killedTxt.faceColor = Color.yellow; 
            record = true;
        }
     
        if (PlayerPrefs.GetInt("dicesRolled") < dicesRolled)
        {
            rolledTxt.faceColor = Color.yellow; 
            record = true;
        }
     
        if (PlayerPrefs.GetInt("dicesBurned") < dicesBurned)
        {
            burnedTxt.faceColor = Color.yellow; 
            record = true;
        }
            
        if (record)
            recordTxt.SetActive(true);

        PlayerPrefs.SetInt("killedEnemys", killedEnemys);
        PlayerPrefs.SetInt("dicesRolled", dicesRolled);
        PlayerPrefs.SetInt("dicesBurned", dicesBurned);
        PlayerPrefs.SetFloat("totalTime", totalTime);

        Time.timeScale = 0;
        timeTxt.text = "Time: " + stringTime;
        killedTxt.text = "Killed Enemys: " + killedEnemys.ToString();
        rolledTxt.text = "Rolled Dices: " + dicesRolled.ToString();
        burnedTxt.text = "Burned Dices: " + dicesBurned.ToString();
        
    }
}
