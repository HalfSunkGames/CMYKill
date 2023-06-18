using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    public static DiceController Instance;

    public int currentValue;                            // Dice current face   

    public Dictionary<int, bool> diceSidesStatus;       // Status of dice faces (true - usable, false - burnt)
    public Dictionary<int, int> diceSidesUses;          // Uses of each attack (1-3 one use, 4-6 five uses)
    [SerializeField] private int diceSides = 6;         // D6   
    private int secondsToWaitRoll = 1;                  // Seconds the dice keeps rolling (and cannot be rolled again)
    private int secondsToWaitReset = 1;                 // Seconds the reset animation lasts
    private bool isRolling = false;                     // Is the dice rolling?

    [SerializeField] private List<Sprite> diceSidesUsableIcons;
    [SerializeField] private List<Sprite> diceSidesBurntIcons;
    [SerializeField] private List<Sprite> attackIcons;  // 0-2 - melee,  3-5 - ranged

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitializeDice();
    }

    // Create dice status dictionary and assign a starting value
    private void InitializeDice()
    {
        // Initialize status
        diceSidesStatus = new Dictionary<int, bool>();

        for (int i = 1; i < diceSides + 1; i++)
        {
            diceSidesStatus.Add(i, true);
        }

        currentValue = Random.Range(1, diceSides + 1);  // D6 range (1-6)

        // Initialize cooldowns
        diceSidesUses = new Dictionary<int, int>();

        for (int i = 1; i < diceSides + 1; i++)
        {
            if (i < 4)
                diceSidesUses.Add(i, 1);    // One use for melee attacks
            else
                diceSidesUses.Add(i, 5);    // Five uses for ranged attacks
        }

        UIController.Instance.StopRollDiceHUD();    // Cancels any rolling animation
        UIController.Instance.UnlockDice();         // Disables the lock in UI
        SetDiceHUD();
        SetAttackHUD();
    }

    // Just roll the D6 dice
    public bool RollDice()
    {
        // Cannot dice roll
        if (isRolling ||                        // The dice is rolling
            (currentValue > 3 && 
            diceSidesUses[currentValue] < 5 &&  // The attack is used and not fully burnt
            diceSidesUses[currentValue] > 0))   
        {
            // Play some animation/sound to show you cannot roll yet
            Debug.Log("Cannot roll");
            AudioVFXController.Instance.PlayCantRoll();
            return false;
        }
        else
        {
            // The dice is stricked!!
            // If the side is burnt, secretly rolls again. If that roll is also burnt, that's bad luck :)
            currentValue = Random.Range(1, diceSides + 1);
            if (diceSidesStatus[currentValue] == false) currentValue = Random.Range(1, diceSides + 1);

            // Wait for the coroutine to end (the dice is rolling)
            StartCoroutine(WaitAndRoll());

            return true;
        }
    }

    private IEnumerator WaitAndRoll()
    {
        // Wait a second to roll the dice
        isRolling = true;

        Debug.Log("Rolling...");

        // Play some rolling animation...
        UIController.Instance.StartRollDiceHUD();

        AudioVFXController.Instance.PlayDiceRoll();
        yield return new WaitForSeconds(secondsToWaitRoll);

        // Set the dice face in the HUD icon
        UIController.Instance.StopRollDiceHUD();
        UIController.Instance.UnlockDice();
        SetDiceHUD();
        SetAttackHUD();
        ScoreStatus.Instance.dicesRolled++;

        isRolling = false;

        Debug.Log($"Rolled: {currentValue}");
    }

    // Checks if the dice must be reseted
    private bool TimeToResetDice(int currentValue)
    {
        bool reset = true;

        for (int i = 1; i < diceSides + 1; i++)
        {
            // Do not count the current value
            if (currentValue == i) continue;

            // If another attack is up, do not reset the dice
            if (diceSidesStatus[i] == true)
                reset = false;
        }

        return reset;
    }

    // Reset dice values (make attacks available again)
    private IEnumerator ResetDice(int currentValue)
    {
        Debug.Log("Resetting...");
        // Play some animation in the HUD...
        // ...
        AudioVFXController.Instance.PlayDiceNew();
        // This yield is to wait for the animation to end
        yield return new WaitForSeconds(secondsToWaitReset);

        for (int i = 1; i < diceSides + 1; i++)
        {
            if (currentValue == i) continue;
            diceSidesStatus[i] = true;
        }

        for (int i = 1; i < diceSides + 1; i++)
        {
            if (i < 4)
                diceSidesUses[i] = 1;    // One use for melee attacks
            else
                diceSidesUses[i] = 5;    // Five uses for ranged attacks
        }

        ScoreStatus.Instance.dicesBurned++;

        Debug.Log("Reset complete");
    }

    public bool UseAttack(int attack)
    {
        if (diceSidesStatus[attack] == false) return false; // The attack can't be used!

        // If the attack can be used...
        if (diceSidesUses[attack] > 0)
        {
            diceSidesUses[attack]--;            // Decrease uses
            SetAttackHUD();
            UIController.Instance.LockDice();
        }   

        // If this use was the last one...
        if (diceSidesUses[attack] == 0)
        {
            diceSidesStatus[attack] = false;    // Disable the attack
            SetDiceHUD();
            UIController.Instance.UnlockDice();

            // Check if the dice have to be reseted
            if (TimeToResetDice(currentValue))
            {
                StartCoroutine(ResetDice(currentValue));
                ScoreStatus.Instance.dicesBurned++;
            }                
        }

        return true;
    }

    private void SetAttackHUD()
    {
        UIController.Instance.SetAttacksHUD(diceSidesUses[currentValue].ToString());

        if (currentValue < 4)
            UIController.Instance.SetAttackSpriteHUD(attackIcons[currentValue - 1]);
        else
            UIController.Instance.SetAttackSpriteHUD(attackIcons[currentValue - 1]);
    }

    private void SetDiceHUD()
    {
        // Set the icon in the HUD, depending if it's burned or not
        if (diceSidesStatus[currentValue] == true)
            UIController.Instance.SetDiceHUD(diceSidesUsableIcons[currentValue - 1]);
        else if(diceSidesStatus[currentValue] == false)
            UIController.Instance.SetDiceHUD(diceSidesBurntIcons[currentValue - 1]);
    }


}
