using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] ButtonType buttonType;
    [SerializeField] ButtonFunction buttonFunction;
    [SerializeField] Door doorToUnlock;

    MenuManager menuManager;

    private void Start()
    {
        if (buttonFunction == ButtonFunction.TriggerWin)
        {
            menuManager = FindObjectOfType<MenuManager>();
        }
    }

    public ButtonType GetButtonType() 
    { 
        return buttonType; 
    }

    public ButtonFunction GetButtonFunction()
    {
        return buttonFunction;
    }

    public void UseButton()
    {
        SoundManager.PlaySound(SoundAssets.instance.button, 1f);
        switch (buttonFunction)
        {
            case ButtonFunction.UnlockDoor:
                doorToUnlock.OpenDoor();
                break;
            case ButtonFunction.TriggerWin:
                menuManager.WinGame();
                break;
            default:
                break;
        }
    }
}