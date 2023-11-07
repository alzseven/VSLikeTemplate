using System;
using Data.Item;
using UnityEngine;

namespace Contents
{
    [Serializable]
    public class InventoryItem
    {
        public ItemData itemData;
        //TODO: Abstract class?
        public GameObject itemInstance;
        public int itemLevel;
    }
}