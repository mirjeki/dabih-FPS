using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] int healAmount = 25;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth.GetCurrentHealth() < playerHealth.GetMaxHealth())
            {
                SoundManager.PlaySound(SoundAssets.instance.medkit, 0.6f);
                other.GetComponent<PlayerHealth>().GainHealth(healAmount);
                Destroy(gameObject);
            }
        }
    }
}
