using System;
using UnityEngine;
using UnityEngine.Events;

namespace Shrewd
{
    public class DraggedItemManager : MonoBehaviour
    {

        private bool isDraggingItem;
        private GameObject draggedItem;
        private Vector2 itemLocalPositionBeforeDrag;
        private Transform itemParentBeforeDrag;

        private Action<GameObject> inventoryItemDragBeginListener;
        private Action<GameObject> inventoryItemDragEndListener;

        private void OnEnable()
        {
            // Listen for when inventory items are dragged.
            inventoryItemDragBeginListener += AnchorItemToSelf;
            EventManager.StartListener(EventName.INVENTORY_ITEM_DRAG_BEGIN, inventoryItemDragBeginListener);
            // Listen for when inventory items stop being dragged.
            inventoryItemDragEndListener += UnanchorItemFromSelf;
            EventManager.StartListener(EventName.INVENTORY_ITEM_DRAG_END, inventoryItemDragEndListener);
        }

        private void OnDisable()
        {
            // Clean up listeners.
            EventManager.StopListener(EventName.INVENTORY_ITEM_DRAG_BEGIN, inventoryItemDragBeginListener);
            EventManager.StopListener(EventName.INVENTORY_ITEM_DRAG_END, inventoryItemDragEndListener);
        }

        private void AnchorItemToSelf(GameObject item)
        {
            this.isDraggingItem = true;
            // Save some of item's information for later when the player drops the item.
            this.itemParentBeforeDrag = item.transform.parent;
            this.itemLocalPositionBeforeDrag = item.transform.localPosition;
            this.draggedItem = item;

            // Init own position so item appears in correct position for first frame.
            this.SetOwnPositionToMouse();

            // Instead of having the item snap to the center of the cursor, here we calculate and maintain the offset between the cursor and item when it i was first clicked.
            // Because setting the item as our child changes our position, we calculate the offset between the mouse and the item BEFORE setting its parent.
            Vector2 mouseToItemPositionOffset = item.transform.position - Input.mousePosition;
            item.transform.SetParent(transform, false);
            item.transform.localPosition = new Vector3(mouseToItemPositionOffset.x, mouseToItemPositionOffset.y, item.transform.position.z);
        }

        private void UnanchorItemFromSelf(GameObject item)
        {
            // Reset item to original position before drag.
            this.draggedItem.transform.SetParent(this.itemParentBeforeDrag);
            this.draggedItem.transform.localPosition = this.itemLocalPositionBeforeDrag;

            // Clean up references.
            this.draggedItem = null;
            this.isDraggingItem = false;
            this.itemLocalPositionBeforeDrag = new Vector2();
            this.itemParentBeforeDrag = null;
    }

        private void Update()
        {
            if (isDraggingItem)
            {
                this.SetOwnPositionToMouse();
            }

        }

        private void SetOwnPositionToMouse()
        {
            this.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.transform.position.z);
        }
    }


}
