using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float meleeDamage = 20f;
    [SerializeField] float rangedDamage = 3f;
    [SerializeField] float rangedAttackMissChance = 33.3f;
    [SerializeField] float flashTimer = 0.05f;
    [SerializeField] float attackDelay = 0.2f;
    [SerializeField] bool hasMuzzleFlash;
    PlayerHealth target;
    MeshRenderer muzzleFlash;
    Light muzzleFlashLight;
    EnemyAI enemyAI;
    EnemyType enemyType;
    private float muzzleFlashDeactivateTime = 0f;
    private float nextAttackCycle;

    private void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
        if (hasMuzzleFlash)
        {
            muzzleFlash = HelperMethods.GetChildGameObject(transform.gameObject, "MuzzleFlash").GetComponent<MeshRenderer>();
            muzzleFlashLight = muzzleFlash.GetComponentInChildren<Light>();
        }
        enemyAI = GetComponent<EnemyAI>();
        enemyType = enemyAI.GetEnemyType();
    }

    private void Update()
    {
        if (hasMuzzleFlash && muzzleFlash.enabled)
        {
            if (Time.time > muzzleFlashDeactivateTime)
            {
                ToggleMuzzleFlash();
            }
        }
    }

    void RangedAttack()
    {
        if (target == null) 
        {
            return;
        }

        if (hasMuzzleFlash)
        {
            muzzleFlashDeactivateTime = Time.time + flashTimer;
            ToggleMuzzleFlash();
        }

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

        float hitPercentage = Random.Range(1f, 100f);

        bool hit = hitPercentage > rangedAttackMissChance ? true : false;
        if (hit)
        {
            target.TakeDamage(rangedDamage);
        }
    }

    private void ToggleMuzzleFlash()
    {
        muzzleFlash.enabled = !muzzleFlash.enabled;
        muzzleFlashLight.enabled = !muzzleFlashLight.enabled;
    }

    void MeleeAttack()
    {
        if (target == null)
        {
            return;
        }

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

        target.TakeDamage(meleeDamage);
    }
}
