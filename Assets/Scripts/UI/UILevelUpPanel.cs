using System.Collections.Generic;
using Contents;
using Contents.Manager;
using Contents.Player;
using Data.Item;
using Data.Item.Loot;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class UILevelUpPanel : MonoBehaviour
    {
        [SerializeField] private LootTable lootTable;
        [SerializeField] private int itemCount;
        
        [SerializeField] private ItemInventory weaponInventory;
        // [SerializeField] private ItemInventory passiveItemInventory;
        //TODO:
        public UnityEvent onShowPanel;

        private RectTransform _rectTransform;
        private UILevelUpButton[] _levelUpButtons;
        
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _levelUpButtons = GetComponentsInChildren<UILevelUpButton>(true);

            weaponInventory.itemDataList = new List<InventoryItem>();
            
            Hide();
        }

        private void OnEnable()
        {
            GameManager.Instance.OnLevelUp += Show;
        }

        //TODO: Move OnLevelUp to somewhere else
        private void OnDisable()
        {
            GameManager.Instance.OnLevelUp -= Show;
        }

        public void Show(int level)
        {
            onShowPanel?.Invoke();
            PickItems();
            _rectTransform.localScale = Vector3.one;
            
        }

        public void Hide()
        {
            _rectTransform.localScale = Vector3.zero;
        }
        
        //TODO: Optimize
        private void PickItems()
        {
            var pickedItems = new List<ItemData>();
            var tempRandoms = new List<int>();
            var ables = new List<ItemData>();
            
            if (weaponInventory.itemDataList.Count < weaponInventory.maxItemCount)
            {
                foreach (var item in lootTable.loot)
                {
                    var skip = false;
                    foreach (var inventoryItem in weaponInventory.itemDataList)
                    {
                        if (inventoryItem.itemData.itemId == item.lootableItem.itemId 
                            && inventoryItem.itemLevel >= 3)
                        {
                            skip = true;
                            break;
                        }
                    }
                    if (!skip && ables.Contains(item.lootableItem) == false) ables.Add(item.lootableItem);
                }

                if (ables.Count < _levelUpButtons.Length)
                {
                    pickedItems.AddRange(ables);
                }
                else
                {
                    var randItems = lootTable.GetRandomWeightedUniqueItems(itemCount);
                    pickedItems.AddRange(randItems);
                }
            }
            else // if (weaponInventory.itemDataList.Count == weaponInventory.maxItemCount)
            {
                foreach (var item in weaponInventory.itemDataList)
                {
                    //TODO: Replace literal
                    if (item.itemLevel < 3)
                    {
                        ables.Add(item.itemData);
                    }
                }

                if (ables.Count < _levelUpButtons.Length)
                {
                    pickedItems.AddRange(ables);
                }
                else
                {
                    while (pickedItems.Count < _levelUpButtons.Length)
                    {
                        // Should pick items in inventory only
                        // TODO: Weight
                        var tempRand = Random.Range(0, ables.Count);

                        if (tempRandoms.Contains(tempRand) == false)
                        {
                            tempRandoms.Add(tempRand);
                            pickedItems.Add(ables[tempRand]);
                        }
                
                    }
                }
                
            }

            for (var index = 0; index < pickedItems.Count; index++)
            {
                var itemData = pickedItems[index];
                var level = 0;
                foreach (var inventoryItem in weaponInventory.itemDataList)
                {
                    if (inventoryItem.itemData.itemId == itemData.itemId)
                    {
                        level = inventoryItem.itemLevel;
                    }
                }
                _levelUpButtons[index].Initialize(itemData, level);
            }

            for (int i = pickedItems.Count; i < itemCount; i++)
            {
                _levelUpButtons[i].Clear();
            }
        }
    }
}