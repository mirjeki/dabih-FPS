using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] Sprite icon;
    [SerializeField] ItemType itemType;

    public Sprite GetIcon()
    {
        return icon;
    }

    public ItemType GetItemType()
    {
        return itemType;
    }
}
