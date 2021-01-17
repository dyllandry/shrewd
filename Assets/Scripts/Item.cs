using UnityEngine;

namespace Shrewd
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
    public class Item : ScriptableObject
    {
        public string itemName;
        public string description;
        public Vector2 inventorySize;
        public Sprite sprite;
    }
}
