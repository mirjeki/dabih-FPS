using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Interactive : MonoBehaviour
{
    [SerializeField] InteractiveType interactiveType;
    [SerializeField] string interactiveTag;
    [SerializeField] bool pickUp;
    [SerializeField] bool disabled;
    [SerializeField] private GameObject selectUI;
    private bool canUse;
    Button button;
    InventoryItem inventoryItem;
    FirstPersonController firstPersonController;
    GameObject mainCamera;
    DialogueTrigger dialogueTrigger;
    MissionEventTrigger missionEventTrigger;

    //the entity in proximity to the interactive
    Collider actor;

    private void Start()
    {
        switch (interactiveType)
        {
            case InteractiveType.Button:
                button = GetComponent<Button>();
                break;
            case InteractiveType.Pickup:
                inventoryItem = GetComponent<InventoryItem>();
                break;
            default:
                break;
        }

        dialogueTrigger = GetComponent<DialogueTrigger>();
        missionEventTrigger = GetComponent<MissionEventTrigger>();

        firstPersonController = FindObjectOfType<FirstPersonController>();

        if (mainCamera == null)
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    private void Update()
    {
        if (firstPersonController.IsUsing && canUse)
        {
            UseInteractive();
        }
    }

    public string GetInteractiveTag()
    {
        return interactiveTag;
    }

    public void SetInteractiveTag(string newTag)
    {
        interactiveTag = newTag;
    }

    public bool GetDisabled()
    {
        return disabled;
    }

    public void SetDisabled(bool newValue)
    {
        disabled = newValue;
    }

    private void UseInteractive()
    {
        if (dialogueTrigger != null)
        {
            dialogueTrigger.Use();
        }
        if (missionEventTrigger != null)
        {
            missionEventTrigger.Use();
        }

        switch (interactiveType)
        {
            case InteractiveType.Button:
                UseButton();
                break;
            case InteractiveType.Pickup:
                actor.GetComponent<Inventory>().SetInventoryItem(inventoryItem);
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }

    private void UseButton()
    {
        if (button.GetButtonType() == ButtonType.UseOnce)
        {
            disabled = true;
            selectUI.SetActive(false);
            canUse = false;
        }

        button.UseButton();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (disabled)
        {
            return;
        }

        actor = other;
        if (other.transform.root.tag == CommonStrings.playerString)
        {
            //transform.forward = mainCamera.transform.forward;
            selectUI.SetActive(true);
            canUse = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        actor = null;
        if (other.transform.root.tag == CommonStrings.playerString)
        {
            selectUI.SetActive(false);
            canUse = false;
        }
    }
}
