using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] private TextMeshProUGUI enemiesText;   // Enemies killed text in HUD
    [SerializeField] private TextMeshProUGUI attacksText;   // Attacks remaining text in HUD

    [SerializeField] private Image diceImage;
    [SerializeField] private Image attackImage;

    [SerializeField] private GameObject diceSide;
    [SerializeField] private GameObject diceAnim;

    [SerializeField] private GameObject lockImage;

    [SerializeField] private List<GameObject> hearts;

    private void Awake()
    {
        Instance = this;
    }

    public void SetDiceHUD(Sprite diceSprite)
    {
        diceImage.sprite = diceSprite;
    }

    public void StartRollDiceHUD()
    {
        diceSide.SetActive(false);
        diceAnim.SetActive(true);
    }

    public void StopRollDiceHUD()
    {
        diceSide.SetActive(true);
        diceAnim.SetActive(false);
    }

    public void LockDice()
    {
        lockImage.SetActive(true);
    }

    public void UnlockDice()
    {
        lockImage.SetActive(false);
    }

    public void SetEnemiesHUD(int enemies)
    {
        enemiesText.SetText(enemies.ToString());
    }

    public void SetAttacksHUD(string attacks)
    {
        attacksText.SetText(attacks.ToString());
    }

    public void SetAttackSpriteHUD(Sprite attackSprite)
    {
        attackImage.sprite = attackSprite;
    }

    public void UpdateHealth(int health)
    {
        switch(health)
        {
            case 3:
                hearts[0].SetActive(true);
                hearts[1].SetActive(true);
                hearts[2].SetActive(true);
                break;
            case 2:
                hearts[0].SetActive(true);
                hearts[1].SetActive(true);
                hearts[2].SetActive(false);
                break;
            case 1:
                hearts[0].SetActive(true);
                hearts[1].SetActive(false);
                hearts[2].SetActive(false);
                break;
            case 0:
                hearts[0].SetActive(false);
                hearts[1].SetActive(false);
                hearts[2].SetActive(false);
                break;
        }
    }
}
