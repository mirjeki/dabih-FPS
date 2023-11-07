using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum MissionEventType
{
    spawnEnemies,
    setInteractive
}

public enum SpawnEvents
{
    Spawn1,
    Spawn2
}

public class MissionEventTrigger : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] List<MissionEventType> missionEventsToTrigger;
    [SerializeField] bool triggerEnter;
    [Header("Enemy Spawning")]
    [SerializeField] List<SpawnEvents> spawnEvents;
    [SerializeField] GameEvent onEnemiesSpawned;
    [Header("Set Interactive")]
    [SerializeField] GameEvent onSetInteractive;
    [SerializeField] bool enable;

    public void Use()
    {
        if (triggerEnter)
        {
            return;
        }

        TriggerEvents();
    }

    private void TriggerEvents()
    {
        foreach (var missionEvent in missionEventsToTrigger)
        {
            switch (missionEvent)
            {
                case MissionEventType.spawnEnemies:
                    onEnemiesSpawned.Raise(this, spawnEvents);
                    break;
                case MissionEventType.setInteractive:
                    onSetInteractive.Raise(this, enable);
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggerEnter)
        {
            return;
        }

        if (other.transform.root.tag == CommonStrings.playerString)
        {
            TriggerEvents();
        }
    }
}
