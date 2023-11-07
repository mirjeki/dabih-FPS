using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] bool menuOnly;
    [SerializeField] Object level;
    [SerializeField] Object winScreen;
    FirstPersonController firstPersonController;

    private void Start()
    {
        if (!menuOnly)
        {
            firstPersonController = FindObjectOfType<FirstPersonController>();
        }
    }

    public void StartGame()
    {
        //SceneManager.LoadScene(level.name.Replace(" ", string.Empty));

        SceneManager.LoadScene("FacilityAssault");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ResumeGame()
    {
        if (firstPersonController != null)
        {
            firstPersonController.TogglePause();
        }
    }

    public void ReloadGame()
    {
        Time.timeScale = 1.0f;
        FindObjectOfType<WeaponSwitcher>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (firstPersonController != null)
        {
            firstPersonController.IsPaused = true;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void WinGame()
    {
        //SceneManager.LoadScene(winScreen.name.Replace(" ", string.Empty));
        SceneManager.LoadScene("WinScreen");

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
