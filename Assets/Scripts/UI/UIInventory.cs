using Contents;
using UnityEngine;

namespace UI
{
    //TODO: Reveal when LevelUp
    public class UIInventory : MonoBehaviour
    {
        public ItemInventory inventory;
        public UIInventoryItem[] slots;
        private RectTransform _rectTransform;
        
        private void Awake()
        {
            slots = GetComponentsInChildren<UIInventoryItem>();
            _rectTransform = GetComponent<RectTransform>();

            Hide();
        }

        public void Show()
        {
            UpdateInventory();
            _rectTransform.localScale = Vector3.one;
            
        }

        public void Hide()
        {
            _rectTransform.localScale = Vector3.zero;
        }

        private void UpdateInventory()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (i < inventory.itemDataList.Count)
                {
                    slots[i].gameObject.SetActive(true);
                    slots[i].UpdateItemDisplay(inventory.itemDataList[i].itemData.itemIcon, inventory.itemDataList[i].itemLevel);
                }
                else 
                {
                    slots[i].gameObject.SetActive(false);
                }
            }
        }
    }
}