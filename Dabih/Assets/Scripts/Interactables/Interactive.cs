using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Interactive : MonoBehaviour
{
    [SerializeField] string itemDescription;
    [SerializeField] bool pickUp;
    [SerializeField] private GameObject buttonUI;
    private bool canUse;
    InventoryItem inventoryItem;
    FirstPersonController firstPersonController;
    GameObject mainCamera;

    //the entity in proximity to the interactive
    Collider actor;

    private const string playerString = "Player";

    private void Start()
    {
        if (pickUp)
        {
            inventoryItem = GetComponent<InventoryItem>();
        }
        firstPersonController = FindObjectOfType<FirstPersonController>();

        if (mainCamera == null)
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    private void Update()
    {
        if (firstPersonController.IsUsing)
        {
            UseInteractive();
        }
    }


    public string GetItemDescription()
    {
        return itemDescription;
    }

    private void UseInteractive()
    {
        if (!canUse)
        {
            return;
        }

        if (pickUp)
        {
            actor.GetComponent<Inventory>().SetInventoryItem(inventoryItem);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log($"{transform.name} used");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        actor = other;
        if (other.transform.root.tag == playerString)
        {
            transform.forward = mainCamera.transform.forward;
            buttonUI.SetActive(true);
            canUse = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        actor = null;
        if (other.transform.root.tag == playerString)
        {
            buttonUI.SetActive(false);
            canUse = false;
        }
    }
}
