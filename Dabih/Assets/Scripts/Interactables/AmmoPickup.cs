using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] AmmoType ammoType;
    [SerializeField] int ammoAmount = 5;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.tag == CommonStrings.playerString)
        {
            SoundManager.PlaySound(SoundAssets.instance.ammo, 0.25f);
            other.GetComponent<Ammo>().ModifyAmmoAmount(ammoAmount, ammoType);
            var dialogueTrigger = GetComponent<DialogueTrigger>();
            if (dialogueTrigger != null)
            {
                dialogueTrigger.Use();
            }
            Destroy(gameObject);
        }
    }
}
