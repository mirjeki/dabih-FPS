using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthUI;
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float currentHealth = 100f;

    private void Start()
    {
        currentHealth = maxHealth;
        healthUI.text = currentHealth.ToString();
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

        if (currentHealth < 0f)
        {
            Debug.Log("Game over!");
        }
    }

    public void GainHealth(float health)
    {
        currentHealth += health;
        healthUI.text = currentHealth.ToString();
    }
}
