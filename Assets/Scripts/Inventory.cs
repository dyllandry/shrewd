using UnityEngine;

namespace Shrewd
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "ScriptableObjects/Inventory")]
    public class Inventory : ScriptableObject
    {
        public Vector2 gridSize;
        public Vector2 cellPixelSize;
        public InventoryItemLayout itemLayout;
    }

    [System.Serializable]
    public class InventoryItemLayout
    {
        public PlacedInventoryItem[] placedInventoryItems;
    }

    [System.Serializable]
    public class PlacedInventoryItem
    {
        public Item item;
        public Vector2 position;
    }
}
