using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;
    FirstPersonController firstPersonController;

    void Start()
    {
        gameOverCanvas.enabled = false;
        firstPersonController = GetComponent<FirstPersonController>();
    }

    public void HandleDeath()
    {
        gameOverCanvas.enabled = true;
        firstPersonController.IsPaused = true;
        Time.timeScale = 0f;
        FindObjectOfType<WeaponSwitcher>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
