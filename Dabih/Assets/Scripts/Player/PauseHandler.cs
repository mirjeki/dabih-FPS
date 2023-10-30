using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    [SerializeField] Canvas pauseCanvas;
    bool isPaused = false;

    void Start()
    {
        pauseCanvas.enabled = false;
    }

    public bool HandlePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }

        return isPaused;
    }

    private void PauseGame()
    {
        pauseCanvas.enabled = true;
        Time.timeScale = 0f;
        FindObjectOfType<WeaponSwitcher>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void ResumeGame()
    {
        pauseCanvas.enabled = false;
        Time.timeScale = 1.0f;
        FindObjectOfType<WeaponSwitcher>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
