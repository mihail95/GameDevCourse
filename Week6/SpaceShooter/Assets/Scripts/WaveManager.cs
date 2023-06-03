using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class WaveManager : MonoBehaviour
{
    [SerializeField] TMP_Text waveNumberText;
    public GameObject weakEnemyPrefab;
    public GameObject basicEnemyPrefab;
    public GameObject mediumEnemyPrefab;
    public GameObject strongEnemyPrefab;
    public GameObject bossPrefab;
    List<GameObject> enemyShips;

    private int waveNumber;
    private int rest;

    private void Start()
    {
        enemyShips = new List<GameObject>();
        waveNumber = 1;
        SpawnEnemies();
    }

    private void Update()
    {
        if (enemyShips.TrueForAll(x => x == null))
        {
            waveNumber++;
            waveNumberText.text = $"Wave {waveNumber}";
            SpawnEnemies();
        }
    }

    private void SpawnEnemies()
    {
        rest = waveNumber;

        if (waveNumber % 5 == 0)
        {
            GameObject enemyGO = Instantiate(
                    bossPrefab,
                    new Vector3(Random.Range(3, 5), Random.Range(-3, 3), 0),
                    Quaternion.Euler(0, 0, -90));
            enemyShips.Add(enemyGO);
        }
        else
        {
            // Spawn Strong enemies
            int strongEnemyCtr = (int)rest / 7;
            rest %= 7;
            while (strongEnemyCtr > 0)
            {
                GameObject enemyGO = Instantiate(
                    strongEnemyPrefab,
                    new Vector3(Random.Range(1, 8), Random.Range(-4, 4), 0),
                    Quaternion.Euler(0, 0, -90));

                enemyShips.Add(enemyGO);
                strongEnemyCtr--;
            }

            // Spawn Medium enemies
            int mediumEnemyCtr = (int)rest / 5;
            rest %= 5;
            while (mediumEnemyCtr > 0)
            {
                GameObject enemyGO = Instantiate(
                    mediumEnemyPrefab,
                    new Vector3(Random.Range(1, 8), Random.Range(-4, 4), 0),
                    Quaternion.Euler(0, 0, -90));

                enemyShips.Add(enemyGO);
                mediumEnemyCtr--;
            }

            // Spawn Weak enemies
            int basicEnemyCtr = (int)rest / 3;
            rest %= 3;
            while (basicEnemyCtr > 0)
            {
                GameObject enemyGO = Instantiate(
                    weakEnemyPrefab,
                    new Vector3(Random.Range(1, 8), Random.Range(-4, 4), 0),
                    Quaternion.Euler(0, 0, -90));

                enemyShips.Add(enemyGO);
                basicEnemyCtr--;
            }

            // Spawn Basic enemies
            int weakEnemyCtr = rest;
            while (weakEnemyCtr > 0)
            {
                GameObject enemyGO = Instantiate(
                    basicEnemyPrefab,
                    new Vector3(Random.Range(1, 8), Random.Range(-4, 4), 0),
                    Quaternion.Euler(0, 0, -90));

                enemyShips.Add(enemyGO);
                weakEnemyCtr--;
            }
        }
    }

    public int GetCurrentWave()
    {
        return waveNumber-1;
    }
}
