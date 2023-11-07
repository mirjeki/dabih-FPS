using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.tag == CommonStrings.playerString)
        {
            Destroy(gameObject);
        }
    }
}
