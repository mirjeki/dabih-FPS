using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] float deathTimer = 1f;
    bool isAlive;

    void Start()
    {
        isAlive = true;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    void Update()
    {
        ProcessDeath();
    }

    private void ProcessDeath()
    {
        if (isAlive &&  health <= 0) 
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        yield return new WaitForSecondsRealtime(deathTimer);

        isAlive = false;
        //play death animation
        Destroy(gameObject);
    }
}
