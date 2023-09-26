using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float turnSpeed = 5f;
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float shootingRange = 30f;

    NavMeshAgent agent;
    Animator animator;
    EnemyHealth health;
    float distanceToTarget = Mathf.Infinity;
    bool chasingTarget;
    bool isProvoked;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        health = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        ResetAnims();
        if (!health.GetIsAlive())
        {
            agent.isStopped = true;
        }
        else
        {
            distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (isProvoked)
            {
                EngageTarget();
            }
            else if (distanceToTarget < chaseRange)
            {
                isProvoked = true;
            }

            if (distanceToTarget > 30f)
            {
                isProvoked = false;
            }

            if (chasingTarget)
            {
                animator.SetFloat("Speed", agent.speed);
                animator.SetBool("IsWalking", true);
            }

            if (agent.velocity == Vector3.zero)
            {
                chasingTarget = false;
            }
        }
    }

    private void EngageTarget()
    {
        FaceTarget();
        if (distanceToTarget <= agent.stoppingDistance)
        {
            AttackTargetMelee();
        }
        else if (distanceToTarget <= shootingRange)
        {
            AttackTargetRanged();
        }
        else if (distanceToTarget >= agent.stoppingDistance)
        {
            ChaseTarget();
        }
    }

    private void AttackTargetMelee()
    {
        animator.SetBool("IsMelee", true);
        agent.ResetPath();
    }

    private void AttackTargetRanged()
    {
        animator.SetBool("IsShooting", true);
        agent.ResetPath();
    }

    private void ChaseTarget()
    {
        agent.SetDestination(target.position);
        animator.SetFloat("Speed", agent.speed);
        animator.SetBool("IsWalking", true);
        chasingTarget = true;
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    private void ResetAnims()
    {
        animator.SetBool("IsMelee", false);
        animator.SetBool("IsShooting", false);
        animator.SetBool("IsWalking", false);
        animator.SetFloat("Speed", 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
