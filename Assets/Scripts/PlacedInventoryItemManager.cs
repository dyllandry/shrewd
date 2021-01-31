using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Shrewd.Events;

namespace Shrewd
{
    public class PlacedInventoryItemManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {

        public PlacedInventoryItem placedInventoryItem;
        public Image itemImage;

        private Vector2 positionBeforeMove;
        private RectTransform rectTransform;

        private InventoryItemDragBegin eventInventoryItemDragBegin;
        private InventoryItemDragEnd eventInventoryItemDragEnd;

        private void OnEnable()
        {
            this.eventInventoryItemDragBegin = new InventoryItemDragBegin();
            this.eventInventoryItemDragEnd = new InventoryItemDragEnd();
    }

        void Start()
        {
            this.itemImage.sprite = this.placedInventoryItem.item.sprite;
            this.itemImage.enabled = true;
            this.rectTransform = GetComponent<RectTransform>();
        }

        public void setPlacedItem(PlacedInventoryItem placedItem)
        {
            this.placedInventoryItem = placedItem;
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            eventInventoryItemDragEnd.Trigger(gameObject);
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            eventInventoryItemDragBegin.Trigger(gameObject);
        }

        // Required for implementing OnBeginDrag & OnEndDrag
        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            return;
        }
    }

}
