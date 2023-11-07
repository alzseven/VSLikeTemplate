using UnityEngine;

namespace Data.Item.Loot
{
    [CreateAssetMenu(fileName = "ItemLootData", menuName = "Data/Item/Loot", order = 1)]
    public class LootData : ScriptableObject
    {
        public ItemData lootableItem;
        public float dropChance;
    }
}