using System;
using UnityEngine;
using UnityEngine.UI;

namespace Shrewd
{
    public class InventoryGridManager : MonoBehaviour
    {
        public Inventory inventory;
        public GameObject placedItemsParent;
        public GameObject inventoryCellsParent;
        private GridLayoutGroup cellsGridLayout;

        void Start()
        {
            cellsGridLayout = inventoryCellsParent.GetComponent<GridLayoutGroup>();
            drawInventoryCells();
            drawPlacedItems();
        }

        private void drawInventoryCells()
        {
            // Setup inventory cells grid layout.
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
                // Here's all the information about the item we are going to place pulled from the inventory scriptable object.
                PlacedInventoryItem placedItemData = inventory.itemLayout.placedInventoryItems[index];

                // Spawn the PlacedItem.
                GameObject placedItem = Instantiate(Resources.Load<GameObject>("Prefabs/Placed Inventory Item"), placedItemsParent.transform);

                // Set the PlacedItem's item information.
                PlacedInventoryItemManager placedItemManager = placedItem.GetComponent<PlacedInventoryItemManager>();
                placedItemManager.setPlacedItem(placedItemData);

                // Size the PlacedItem.
                RectTransform placedItemRect = placedItem.GetComponent<RectTransform>();
                float itemWidth = placedItemData.item.inventorySize.x * inventory.cellPixelSize.x;
                float itemHeight = placedItemData.item.inventorySize.y * inventory.cellPixelSize.y;
                placedItemRect.sizeDelta = new Vector2(itemWidth, itemHeight);

                // Position the PlacedItem.
                Vector2 gridLayoutSpacingOffset = new Vector2(
                    placedItemManager.placedInventoryItem.position.x * cellsGridLayout.spacing.x,
                    placedItemManager.placedInventoryItem.position.y * cellsGridLayout.spacing.y * -1
                );
                float placedItemX = (placedItemManager.placedInventoryItem.position.x * inventory.cellPixelSize.x + itemWidth / 2) + gridLayoutSpacingOffset.x;
                float placedItemY = (placedItemManager.placedInventoryItem.position.y * inventory.cellPixelSize.y * -1 - itemHeight / 2) + gridLayoutSpacingOffset.y;
                placedItemRect.anchoredPosition = new Vector2(placedItemX, placedItemY);
            }
        }
    }


}
