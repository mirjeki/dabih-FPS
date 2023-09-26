using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 25f;
    [SerializeField] float rateOfFire = 0.1f;
    [SerializeField] float flashTimer = 0.05f;
    [SerializeField] GameObject hitEffect;
    private float nextCycle;
    MeshRenderer muzzleFlash;
    private float deactivateTime = 0f;

    private void Start()
    {
        muzzleFlash = HelperMethods.GetChildGameObject(gameObject, "MuzzleFlash").GetComponent<MeshRenderer>();
    }

    public void Shoot(GameObject camera)
    {
        if (Time.time > nextCycle)
        {
            SetMuzzleFlash();

            nextCycle = Time.time + rateOfFire;
            deactivateTime = Time.time + flashTimer;

            RaycastHit hit;
            Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range);

            if (hit.transform != null)
            {
                CreateHitImpact(hit);
                EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
                Debug.Log("Hit: " + hit.transform.name);
            }
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impactFX = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));

        Destroy(impactFX, 0.1f);
    }

    private void Update()
    {
        if (muzzleFlash.enabled)
        {
            if (Time.time > deactivateTime)
            {
                SetMuzzleFlash();
            }
        }
    }

    private void SetMuzzleFlash()
    {
        muzzleFlash.enabled = !muzzleFlash.enabled;
    }
}
