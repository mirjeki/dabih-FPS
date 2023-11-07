using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] SpawnEvents spawnTag;

    public SpawnEvents GetSpawnTag() { return spawnTag; }
}
