using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Collider myCollider;
    MeshRenderer myRenderer;
    private void Start()
    {
        myRenderer = GetComponent<MeshRenderer>();
        myCollider = GetComponent<Collider>();
    }

    public void OpenDoor()
    {
        myCollider.enabled = false;
        myRenderer.enabled = false;
    }

    public void CloseDoor()
    {
        myCollider.enabled = true;
        myRenderer.enabled = true;
    }
}
