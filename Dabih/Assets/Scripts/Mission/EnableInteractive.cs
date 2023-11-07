using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableInteractive : MonoBehaviour
{
    [SerializeField] Interactive interactive;
    public void SetDisabled(Component sender, object data)
    {
        if (data is bool)
        {
            interactive.SetDisabled(!(bool)data);
        }
    }
}
