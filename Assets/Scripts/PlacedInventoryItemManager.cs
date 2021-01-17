using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Shrewd
{
    public class PlacedInventoryItemManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {

        public PlacedInventoryItem placedInventoryItem;
        public Image itemImage;

        private Vector2 positionBeforeMove;
        private RectTransform rectTransform;

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
            EventManager.TriggerEvent(EventName.INVENTORY_ITEM_DRAG_END, gameObject);
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            EventManager.TriggerEvent(EventName.INVENTORY_ITEM_DRAG_BEGIN, gameObject);
        }

        // Must implement for OnBeginDrag & OnEndDrag
        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            return;
        }
    }

}
