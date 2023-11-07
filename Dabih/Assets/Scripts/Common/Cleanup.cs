using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleanup : MonoBehaviour
{
    void Update()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject, 3f);
        }
    }
}
