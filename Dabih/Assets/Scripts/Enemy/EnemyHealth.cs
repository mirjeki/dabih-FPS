using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] float deathTimer = 4f;
    Animator animator;
    bool isAlive;

    void Start()
    {
        isAlive = true;
        animator = GetComponent<Animator>();
    }

    public bool GetIsAlive()
    {
        return isAlive;
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
        if (isAlive && health <= 0) 
        {
            isAlive = false;
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        yield return new WaitForSecondsRealtime(deathTimer);

        animator.SetTrigger("Die");
        //Destroy(gameObject);
    }
}
