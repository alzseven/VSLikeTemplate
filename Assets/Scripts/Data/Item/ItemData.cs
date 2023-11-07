using UnityEngine;

namespace Data.Item
{
    public abstract class ItemData : ScriptableObject
    {
        public int itemId;
        public string itemName;
        public Sprite itemIcon;
        [TextArea] 
        public string itemDescription;

        public virtual string GetItemDesc() => itemDescription;

        public abstract void OnItemSelected();
    }
}