using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacedInventoryItemManager : MonoBehaviour
{

    public PlacedInventoryItem placedInventoryItem;
    public Image itemImage;

    void Start()
    {
        itemImage.sprite = placedInventoryItem.item.sprite;
        itemImage.enabled = true;
    }

    public void setPlacedItem(PlacedInventoryItem placedItem)
    {
        this.placedInventoryItem = placedItem;
    }
}
