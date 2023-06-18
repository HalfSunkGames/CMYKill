using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int health;
    public float invTime; // Tiempo de invulnerabilidad
    public bool invulnerable = false;
    [SerializeField] private GameObject scoreScreen;
    public PlayerController player;
    public AudioSource music;
    
    void Start()
    {
        health = 3;
    }

    public void Damage()
    {
        if (!invulnerable)
        {
            player.DamageAnim();
            health--;
            AudioVFXController.Instance.PlayDamage();
            UIController.Instance.UpdateHealth(health);
            invulnerable = true;
            StartCoroutine(InvulnerableWait());
        }

        if (health == 0)
        {
            EndGame();
        }
    }

    IEnumerator InvulnerableWait()
    {
        yield return new WaitForSeconds(invTime);
        invulnerable = false;
    }

    private void EndGame()
    {
        GetComponent<ScoreStatus>().SaveScore();
        scoreScreen.SetActive(true);
        music.volume = 0;
    }
}
