using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGridManager : MonoBehaviour
{
    public Inventory inventory;
    public GameObject placedItemsParent;
    public GameObject inventoryCellsParent;
    private RectTransform gridRectTransform;

    private void Awake()
    {
        gridRectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        drawInventoryCells();
        drawPlacedItems();
    }

    private void drawInventoryCells()
    {
        // Setup inventory cells grid layout.
        GridLayoutGroup cellsGridLayout = inventoryCellsParent.GetComponent<GridLayoutGroup>();
        cellsGridLayout.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        cellsGridLayout.constraintCount = (int)inventory.gridSize.y;
        cellsGridLayout.cellSize = inventory.cellPixelSize;

        // Spawn inventory cells.
        for (int index = 0; index < inventory.gridSize.x * inventory.gridSize.y; index++)
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/Inventory Cell"), inventoryCellsParent.transform);
        }
    }

    private void drawPlacedItems()
    {
        // Spawn all of the inventory's items in the correct layout.
        for (int index = 0; index < inventory.itemLayout.placedInventoryItems.Length; index++)
        {
            Item item = inventory.itemLayout.placedInventoryItems[index].item;

            // Spawn the PlacedItem.
            GameObject placedItem = Instantiate(Resources.Load<GameObject>("Prefabs/Placed Inventory Item"), placedItemsParent.transform);

            // Set the PlacedItem's item.
            PlacedInventoryItemManager placedItemManager = placedItem.GetComponent<PlacedInventoryItemManager>();
            placedItemManager.placedInventoryItem.item = item;
        }
    }
}

