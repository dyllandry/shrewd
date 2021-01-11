using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacedInventoryItemManager : MonoBehaviour
{

    public PlacedInventoryItem placedInventoryItem;
    public Image itemImage;

    // Start is called before the first frame update
    void Start()
    {
        itemImage.sprite = placedInventoryItem.item.sprite;
        itemImage.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
