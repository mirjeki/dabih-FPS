using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] InventoryItem item;
    [SerializeField] GameObject inventoryUI;
    GameObject inventoryUIIcon;

    private void Start()
    {
        inventoryUIIcon = HelperMethods.GetChildGameObject(inventoryUI, "Icon");
    }

    public InventoryItem GetInventoryItem()
    {
        return item;
    }

    public void SetInventoryItem(InventoryItem newItem)
    {
        item = newItem;
        inventoryUIIcon.GetComponent<Image>().sprite = item.GetIcon();
        ShowInventoryUI();
    }

    public void RemoveInventoryItem()
    {
        item = null;
        inventoryUIIcon.GetComponent<Image>().sprite = null;
        HideInventoryUI();
    }

    private void HideInventoryUI()
    {
        inventoryUI.SetActive(false);
    }

    private void ShowInventoryUI()
    {
        inventoryUI.SetActive(true);
    }

}
