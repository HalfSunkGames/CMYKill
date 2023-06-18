using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVFXController : MonoBehaviour
{
    public static AudioVFXController Instance;

    [SerializeField] List<AudioClip> diceClips;
    [SerializeField] List<AudioClip> playerClips;
    [SerializeField] List<AudioClip> enemyClips;

    [SerializeField] AudioSource diceSource;
    [SerializeField] AudioSource playerSource;
    [SerializeField] AudioSource enemySource;

    private void Awake()
    {
        Instance = this;
    }

    #region Dice Sound
    public void PlayDiceRoll()
    {
        diceSource.clip = diceClips[0];
        diceSource.Play();
    }

    public void PlayDiceChange()
    {
        diceSource.clip = diceClips[1];
        diceSource.Play();
    }

    public void PlayDiceNew()
    {
        diceSource.clip = diceClips[2];
        diceSource.Play();
    }

    public void PlayCantRoll()
    {
        diceSource.clip = diceClips[3];
        diceSource.Play();
    }
    #endregion

    #region Player Sound
    public void PlayShot()
    {
        playerSource.clip = playerClips[0];
        playerSource.Play();
    }

    public void PlayMelee()
    {
        playerSource.clip = playerClips[1];
        playerSource.Play();
    }

    public void PlayDamage()
    {
        playerSource.clip = playerClips[2];
        playerSource.pitch = Random.Range(1.2f, 0.8f);
        playerSource.Play();
        playerSource.pitch = 1;
    }

    #endregion

    #region Enemy Sound
    public void PlayEnemyDeath()
    {
        enemySource.clip = enemyClips[0];
        enemySource.Play();
    }

    public void PlayEnemySpawn()
    {
        enemySource.clip = enemyClips[1];
        enemySource.Play();
    }

    #endregion
}
