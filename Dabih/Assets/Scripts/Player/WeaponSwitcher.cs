using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] WeaponEnum currentWeapon;

    void Start()
    {
        SetActiveWeapon(currentWeapon);
    }

    public GameObject GetActiveWeapon()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Weapon>().GetWeaponType() == currentWeapon)
            {
                return transform.GetChild(i).gameObject;
            }
        }
        return null;
    }

    public GameObject SetActiveWeapon(WeaponEnum chosenWeapon)
    {
        List<GameObject> weapons = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            weapons.Add(transform.GetChild(i).gameObject);
        }

        GameObject newWeapon = null;

        foreach (GameObject weapon in weapons)
        {
            if (chosenWeapon == weapon.GetComponent<Weapon>().GetWeaponType())
            {
                weapon.gameObject.SetActive(true);
                newWeapon = weapon;
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
        }
        return newWeapon;
    }

    void Update()
    {
        WeaponEnum previousWeapon = currentWeapon;
    }
}
