using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 25f;
    [SerializeField] float rateOfFire = 0.1f;
    [SerializeField] float flashTimer = 0.05f;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammo ammo;
    [SerializeField] AmmoType ammoType;
    [SerializeField] TextMeshProUGUI ammoUI;
    [SerializeField] WeaponEnum weaponType;
    private float nextCycle;
    MeshRenderer muzzleFlash;
    Light muzzleFlashLight;
    private float deactivateTime = 0f;


    private void Start()
    {
        muzzleFlash = HelperMethods.GetChildGameObject(gameObject, "MuzzleFlash").GetComponent<MeshRenderer>();
        muzzleFlashLight = muzzleFlash.GetComponentInChildren<Light>();
        ammoUI.text = "Ammo: " + ammo.GetCurrentAmmo(ammoType).ToString();
    }

    public WeaponEnum GetWeaponType()
    {
        return weaponType;
    }

    public void Shoot(GameObject camera)
    {
        if (Time.time > nextCycle && ammo.GetCurrentAmmo(ammoType) > 0)
        {
            ToggleMuzzleFlash();

            switch (GetWeaponType())
            {
                case WeaponEnum.Rifle:
                    SoundManager.PlaySound(SoundAssets.instance.rifleShot, 0.75f);
                    break;
                case WeaponEnum.Shotgun:
                    SoundManager.PlaySound(SoundAssets.instance.shotgunShot, 0.75f);
                    break;
                default:
                    break;
            }


            nextCycle = Time.time + rateOfFire;
            deactivateTime = Time.time + flashTimer;

            RaycastHit hit;
            ammo.ReduceAmmo(ammoType);
            Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range);

            if (hit.transform != null)
            {
                CreateHitImpact(hit);
                EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
                if (target != null)
                {
                    target.TakeDamage(damage, hit);
                }
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
                ToggleMuzzleFlash();
            }
        }
    }

    private void AmmoChanged()
    {
        ammoUI.text = "Ammo: " + ammo.GetCurrentAmmo(ammoType).ToString();
    }

    private void ToggleMuzzleFlash()
    {
        muzzleFlash.enabled = !muzzleFlash.enabled;
        muzzleFlashLight.enabled = !muzzleFlashLight.enabled;
    }
}
