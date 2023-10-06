using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera fpsCamera;
    [SerializeField] float zoomedOutFOV = 40f;
    [SerializeField] float zoomedInFOV = 20f;
    [SerializeField] float zoomedOutMouseSensitivity = 1.5f;
    [SerializeField] float zoomedInMouseSensitivity = 0.5f;

    public float Zoom(bool zoomedIn)
    {
        fpsCamera.m_Lens.FieldOfView = zoomedIn ? zoomedInFOV : zoomedOutFOV;
        return zoomedIn ? zoomedInMouseSensitivity : zoomedOutMouseSensitivity;
    }
}
