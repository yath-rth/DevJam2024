using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    [SerializeField] mapSpawner map;

    [SerializeField] float timeBtwSpawns;
    float timeRn;
    [SerializeField] int waveMultiplier;
    [SerializeField] List<GameObject> enemies = new List<GameObject>();
    int waveCount, waveNumber = 0, currWaveCount;

    private void OnEnable()
    {
        EnemyStats.OnEnemyDeath += despawned;
    }

    private void OnDisable()
    {
        EnemyStats.OnEnemyDeath -= despawned;
    }

    private void Update()
    {
        if (!Player.instance.GetPlayerStats().IsDead)
        {
            if (timeRn < Time.time)
            {
                if (waveCount > 0)
                {
                    spawn();
                }
                else
                {
                    waveNumber++;
                    currWaveCount = waveNumber * waveMultiplier;
                    waveCount = currWaveCount;
                    timeBtwSpawns -= 0.2f;
                }

                timeRn = Time.time + timeBtwSpawns;
            }
        }
    }

    void despawned(int a)
    {
        waveCount--;
    }

    void spawn()
    {
        GameObject _enemy = Instantiate(enemies[Random.Range(0, enemies.Count)], map.spawnPos[Random.Range(0, map.spawnPos.Count)].transform.position, Quaternion.identity);
    }
}
