using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] FirstPersonController firstPersonController;
    public void ReloadGame()
    {
        Time.timeScale = 1.0f;
        FindObjectOfType<WeaponSwitcher>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (firstPersonController != null)
        {
            firstPersonController.enabled = true;
        }

        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
