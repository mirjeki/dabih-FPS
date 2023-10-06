using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float meleeDamage = 20f;
    [SerializeField] float rangedDamage = 3f;
    [SerializeField] float rangedAttackMissChance = 33.3f;
    [SerializeField] float flashTimer = 0.05f;
    [SerializeField] bool hasMuzzleFlash;
    PlayerHealth target;
    MeshRenderer muzzleFlash;
    Light muzzleFlashLight;
    private float muzzleFlashDeactivateTime = 0f;

    private void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
        if (hasMuzzleFlash)
        {
            muzzleFlash = HelperMethods.GetChildGameObject(transform.gameObject, "MuzzleFlash").GetComponent<MeshRenderer>();
            muzzleFlashLight = muzzleFlash.GetComponentInChildren<Light>();
        }
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

        target.TakeDamage(meleeDamage);
    }
}
