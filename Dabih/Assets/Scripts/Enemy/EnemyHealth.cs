using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] ParticleSystem bloodSplatter;
    [SerializeField] ParticleSystem bloodGush;
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

    public void TakeDamage(float damage, RaycastHit hit)
    {
        BroadcastMessage("OnDamageTaken");

        if (bloodSplatter  != null)
        {
            CreateBloodEffect(hit, bloodSplatter);
        }
        if (bloodGush != null)
        {
            CreateBloodEffect(hit, bloodGush);
        }
        health -= damage;
    }

    private void CreateBloodEffect(RaycastHit hit, ParticleSystem effect)
    {
        ParticleSystem impactFX = Instantiate(effect, hit.point, Quaternion.LookRotation(hit.normal));

        impactFX.transform.parent = FindObjectOfType<Cleanup>().transform;

        Destroy(impactFX, 0.1f);
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
            Die();
        }
    }

    private void Die()
    {
        animator.SetTrigger("Die");
        transform.GetComponent<CapsuleCollider>().enabled = false;
        //Destroy(gameObject);
    }
}
