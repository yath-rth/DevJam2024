using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    [SerializeField] mapSpawner map;

    [SerializeField] float timeBtwSpawns;
    float timeRn;
    [SerializeField] int waveMultiplier;
    [SerializeField] GameObject frenchFryEnemy;
    int waveCount, waveNumber = 0, currWaveCount;

    private void Update()
    {
        if (timeRn < Time.time)
        {
            if (waveCount > 0)
            {
                spawn();
                waveCount--;
            }
            else
            {
                waveNumber++;
                currWaveCount = waveNumber * waveMultiplier;
                waveCount = currWaveCount;
            }

            timeRn = Time.time + timeBtwSpawns;
        }
    }

    void spawn()
    {
        GameObject _enemy = Instantiate(frenchFryEnemy, map.spawnPos[Random.Range(0, map.spawnPos.Count)].transform.position, Quaternion.identity);
    }
}
