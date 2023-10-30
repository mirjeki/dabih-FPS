using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] EnemyType enemyType;
    [SerializeField] float turnSpeed = 5f;
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float meleeRange = 6f;
    [SerializeField] float shootingRange = 30f;
    [SerializeField] bool hasRangedAttack;
    [SerializeField] bool hasMeleeAttack;
    [SerializeField] float stumbleGracePeriod = 0.5f;
    [SerializeField] FieldOfView fieldOfView = new FieldOfView(20f, 60f);

    [Header("Audio")]
    [SerializeField] float footstepDelay = 0.2f;
    [SerializeField] float attackDelay = 0.2f;
    [SerializeField] float hurtDelay = 0.2f;

    Transform target;
    bool currentTargetIsVisible;
    NavMeshAgent agent;
    Animator animator;
    EnemyHealth health;
    float stumbleDelay;
    float distanceToTarget = Mathf.Infinity;
    bool chasingTarget;
    bool isProvoked;
    bool isMoving;
    bool isStumbling;
    private float nextStepCycle;
    private float nextAttackCycle;
    private float nextHurtCycle;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        health = GetComponent<EnemyHealth>();
        target = FindObjectOfType<PlayerHealth>().transform;
        InvokeRepeating(nameof(CheckTargets), 0.5f, 0.5f);
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

            if (chasingTarget && distanceToTarget < chaseRange)
            {
                ChaseTarget();
            }

            if (isProvoked && !isStumbling)
            {
                EngageTarget();
            }
            else if (distanceToTarget < fieldOfView.Radius && currentTargetIsVisible)
            {
                isProvoked = true;
            }

            if (distanceToTarget > chaseRange && !currentTargetIsVisible)
            {
                isProvoked = false;
                chasingTarget = false;
            }

            if (agent.velocity == Vector3.zero)
            {
                isMoving = false;
            }
            else
            {
                isMoving = true;
            }

            if (isMoving)
            {
                if (Time.time > nextStepCycle)
                {
                    nextStepCycle = Time.time + footstepDelay;

                    switch (enemyType)
                    {
                        case EnemyType.Alien:
                            SoundManager.PlaySound(SoundAssets.instance.alienWalk, 0.25f);
                            break;
                        case EnemyType.Dog:
                            SoundManager.PlaySound(SoundAssets.instance.dogWalk, 0.25f);
                            break;
                        default:
                            break;
                    }
                }
                animator.SetFloat("Speed", agent.speed);
                animator.SetBool("IsWalking", true);
            }
        }
    }

    private void EngageTarget()
    {
        FaceTarget();

        if (distanceToTarget <= meleeRange && hasMeleeAttack)
        {
            AttackTargetMelee();
        }
        else if (distanceToTarget <= shootingRange && hasRangedAttack && currentTargetIsVisible)
        {
            AttackTargetRanged();
        }
        else if (distanceToTarget >= agent.stoppingDistance)
        {
            ChaseTarget();
        }
    }

    void CheckTargets()
    {
        if (!health.GetIsAlive())
        {
            return;
        }
        currentTargetIsVisible = false;
        Vector3 fieldOfViewPosition = transform.position + transform.up * 1.2f;
        Collider[] targets = GetCollidersInView(fieldOfViewPosition, transform.forward, viewerToIgnore: this.gameObject);

        Transform playerTarget;

        if (targets.Length > 0)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].tag == "Player")
                {
                    playerTarget = targets[i].transform;
                    currentTargetIsVisible = IsVisibleToThisFieldOfView(playerTarget, fieldOfViewPosition, transform.forward);
                    return;
                }
            }
        }
    }

    Collider[] GetCollidersInView(Vector3 position, Vector3 forward, GameObject viewerToIgnore = null)
    {
        List<Collider> colliders = Physics.OverlapSphere(position, fieldOfView.Radius).ToList();

        foreach (Collider col in colliders.ToArray())
        {
            Transform target = col.transform;

            Vector3 targetposition = target.position; targetposition.y = position.y;

            Vector3 directionToTarget = (targetposition - position).normalized;

            if (Vector3.Angle(forward, directionToTarget) > fieldOfView.Vector / 2 || col.gameObject == viewerToIgnore)
            {
                colliders.Remove(col);
            }
        }

        return colliders.ToArray();
    }

    public bool IsVisibleToThisFieldOfView(Transform lookedTarget, Vector3 viewPosition, Vector3 viewForward, float threshold = 0.6f)
    {
        if (lookedTarget == null) return false;

        bool CanSeeTarget = true;
        Vector3 directionToTarget = (lookedTarget.position - viewPosition).normalized;

        //CAN NOT see the target
        if (Vector3.Angle(viewForward, directionToTarget) > fieldOfView.Vector / 2)
        {
            CanSeeTarget = false;
        }
        else
        {
            float normalDistance = Vector3.Distance(viewPosition, lookedTarget.position);
            Vector3 lineCastEndPosition = viewPosition + directionToTarget * normalDistance;

            RaycastHit hit;
            Physics.Linecast(viewPosition, lineCastEndPosition, out hit);
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Player")
                {
                    CanSeeTarget = true;
                }
                else
                {
                    CanSeeTarget = false;
                }
            }
        }
        return CanSeeTarget;
    }

    private void AttackTargetMelee()
    {
        animator.SetBool("IsMelee", true);
        if (Time.time > nextAttackCycle)
        {
            nextAttackCycle = Time.time + attackDelay;
            switch (enemyType)
            {
                case EnemyType.Alien:
                    SoundManager.PlaySound(SoundAssets.instance.alienKick, 0.5f);
                    break;
                case EnemyType.Dog:
                    SoundManager.PlaySound(SoundAssets.instance.dogBite, 0.5f);
                    break;
                default:
                    break;
            }
        }
        agent.isStopped = true;
        agent.ResetPath();
    }

    private void AttackTargetRanged()
    {
        animator.SetBool("IsShooting", true);
        if (Time.time > nextAttackCycle)
        {
            nextAttackCycle = Time.time + attackDelay;
            switch (enemyType)
            {
                case EnemyType.Alien:
                    SoundManager.PlaySound(SoundAssets.instance.alienShot, 0.5f);
                    break;
                case EnemyType.Dog:
                    break;
                default:
                    break;
            }
        }
        agent.isStopped = true;
        agent.ResetPath();
    }

    void OnDamageTaken()
    {
        if (Time.time >= stumbleDelay)
        {
            ResetAnims();
            agent.ResetPath();
            agent.isStopped = true;
            animator.SetBool("Stumble", true);
            isStumbling = true;
            stumbleDelay = Time.time + stumbleGracePeriod;
            if (Time.time > nextHurtCycle)
            {
                nextAttackCycle = Time.time + hurtDelay;
                switch (enemyType)
                {
                    case EnemyType.Alien:
                        SoundManager.PlaySound(SoundAssets.instance.alienHurt, 0.5f);
                        break;
                    case EnemyType.Dog:
                        SoundManager.PlaySound(SoundAssets.instance.dogHurt, 0.5f);
                        break;
                    default:
                        break;
                }
            }
        }
        isProvoked = true;
    }

    private void ChaseTarget()
    {
        agent.SetDestination(target.position);
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
        agent.isStopped = false;
        animator.SetBool("Stumble", false);
        isStumbling = false;
        if (hasMeleeAttack)
        {
            animator.SetBool("IsMelee", false);
        }
        if (hasRangedAttack)
        {
            animator.SetBool("IsShooting", false);
        }
        animator.SetBool("IsWalking", false);
        isMoving = false;
        animator.SetFloat("Speed", 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldOfView.Radius);
    }
}
