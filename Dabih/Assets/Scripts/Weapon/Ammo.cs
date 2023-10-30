using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] AmmoSlot[] ammoSlots;

    [System.Serializable]
    private class AmmoSlot
    {
        public AmmoType ammoType;
        public int ammoAmount = 10;
    }

    public int GetCurrentAmmo(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).ammoAmount;
    }

    public void ModifyAmmoAmount(int amount, AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).ammoAmount += amount;
        BroadcastMessage("AmmoChanged");
    }

    public void ReduceAmmo(AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).ammoAmount--;
        BroadcastMessage("AmmoChanged");
    }

    private AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        foreach (var ammo in ammoSlots)
        {
            if (ammo.ammoType == ammoType)
            {
                return ammo;
            }
        }
        return null;
    }
}
