using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;

    NavMeshAgent agent;
    float distanceToTarget = Mathf.Infinity;
    bool chasingTarget;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget < chaseRange || chasingTarget)
        {
            agent.SetDestination(target.position);
            chasingTarget = true;
        }

        if (distanceToTarget > 30f)
        {
            chasingTarget = false;
        }
    }
}
