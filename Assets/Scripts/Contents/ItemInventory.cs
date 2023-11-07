using System;
using System.Collections.Generic;
using UnityEngine;

namespace Contents
{
    [CreateAssetMenu]
    public class ItemInventory : ScriptableObject
    {
        public int maxItemCount;
        public List<InventoryItem> itemDataList;

        public event Action<InventoryItem> onItemAdded;

        public bool AddItem(InventoryItem itemToAdd)
        {
            // Finds an empty slot if there is one
            for (int i = 0; i < itemDataList.Count; i++)
            {
                if (itemDataList[i] == null)
                {
                    itemDataList[i] = itemToAdd;
                    onItemAdded?.Invoke(itemToAdd);
                    return true;
                }
            }
            // Adds a new item if the inventory has space
            if (itemDataList.Count < maxItemCount)
            {
                itemDataList.Add(itemToAdd);
                return true;
            }
            // Debug.Log("No space in the inventory");
            return false;
        }
    
    }
}