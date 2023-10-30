using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthUI;
    [SerializeField] Image bloodSpatter;
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float currentHealth = 100f;
    DeathHandler deathHandler;
    float spatterFadeTime = 1f;
    float spatterTimer;

    private void Start()
    {
        currentHealth = maxHealth;
        healthUI.text = currentHealth.ToString();
        deathHandler = GetComponent<DeathHandler>();
    }

    private void Update()
    {
        DisableSpatter();

    }

    private void DisableSpatter()
    {
        if (Time.time > spatterTimer)
        {
            bloodSpatter.enabled = false;
        }
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthUI.text = currentHealth.ToString();
        bloodSpatter.enabled = true;

        if (Time.time > spatterTimer)
        {
            spatterTimer = Time.time + spatterFadeTime;
            SoundManager.PlaySound(SoundAssets.instance.hurt, 0.5f);
        }

        if (currentHealth <= 0f)
        {
            deathHandler.HandleDeath();
        }
    }

    public void GainHealth(float health)
    {
        currentHealth += health;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthUI.text = currentHealth.ToString();
    }
}
