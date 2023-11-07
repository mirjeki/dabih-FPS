using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] bool spawnDelay;
    [SerializeField] float spawnDelayTimer = 2f;
    List<SpawnEvents> spawnEvents;

    EnemySpawner[] enemySpawners;

    private void Start()
    {
        if (enemySpawners == null)
            enemySpawners = FindObjectsOfType<EnemySpawner>(true);
    }

    public void StartSpawning(Component sender, object data)
    {
        if (data is List<SpawnEvents>)
        {
            spawnEvents = (List<SpawnEvents>)data;
        }

        StartCoroutine(Spawn(spawnDelay ? spawnDelayTimer : 0f));
    }

    public IEnumerator Spawn(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        foreach (SpawnEvents spawn in spawnEvents)
        {
            foreach (EnemySpawner spawner in enemySpawners)
            {
                if (spawn == spawner.GetSpawnTag())
                {
                    spawner.gameObject.SetActive(true);
                }
            }
        }
    }
}
