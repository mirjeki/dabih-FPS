using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] AmmoType ammoType;
    [SerializeField] int ammoAmount = 5;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SoundManager.PlaySound(SoundAssets.instance.ammo, 0.6f);
            other.GetComponent<Ammo>().ModifyAmmoAmount(ammoAmount, ammoType);
            Destroy(gameObject);
        }
    }
}
