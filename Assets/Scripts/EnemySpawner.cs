using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Variables
    public GameObject[] enemys;         // Los tres tipos de enemigos
    public GameObject[] spawnPoints;    // Las cuatro esquinas del escenario

    [SerializeField]private float seconds = 5f; // Delay entre apariciones de enemigos

    
    // Métodos de Unity
    void Start() // Start is called before the first frame update
    {
        StartCoroutine(SpawnEnemy());
        InvokeRepeating("LessSeconds", 1f, 5f);
    }

    // Métodos privados
    private void LessSeconds()  // Disminuye la velocidad de aparición de enemigos gradualmente en base a un porcentaje
    {
            if (seconds >= 2f)
                seconds -= Mathf.Round(((seconds * 7) / 100) * 100) / 100; //Redondea los decimales a dos
            else if (seconds >= 0.2f)
                seconds -= Mathf.Round(((seconds * 3) / 100) * 100) / 100;
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(seconds);                // Espera la cantidad de tiempo
        GameObject spawnedEnemy = enemys[Random.Range(0, 3)];    // Aleatoriza qué enemigo será
        GameObject spawn = spawnPoints[Random.Range(0, 4)];      // Aleatoriza donde aparecerá
        Instantiate(spawnedEnemy, spawn.transform.position, spawn.transform.rotation);
        StartCoroutine(SpawnEnemy());
    }
}
