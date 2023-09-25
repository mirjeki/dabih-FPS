using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float meleeRange = 1f;
    [SerializeField] float shootingRange = 30f;

    NavMeshAgent agent;
    Animator animator;
    float distanceToTarget = Mathf.Infinity;
    bool chasingTarget;
    bool isProvoked;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
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

        if (agent.velocity == Vector3.zero)
        {
            chasingTarget = false;
        }

        if (!chasingTarget)
        {
            animator.SetBool("IsWalking", false);
        }
    }

    private void EngageTarget()
    {
        if (distanceToTarget >= agent.stoppingDistance)
        {
            ChaseTarget();
        }
        else if (distanceToTarget <= agent.stoppingDistance)
        {
            animator.SetBool("IsMelee", true);
            Debug.Log("Hit");
            //StartCoroutine(AttackTargetMelee());
            //animator.SetBool("IsMelee", false);
        }
    }

    private IEnumerator AttackTargetMelee()
    {
        animator.SetBool("IsMelee", true);
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("2HitComboAssaultRifle"))
        {
            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        }
        Debug.Log("Hit");
    }

    private void ChaseTarget()
    {
        agent.SetDestination(target.position);
        animator.SetBool("IsWalking", true);
        chasingTarget = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
