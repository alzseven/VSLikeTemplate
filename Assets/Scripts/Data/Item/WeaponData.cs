using System.Text;
using Contents;
using Contents.Manager;
using Contents.Weapon;
using Data.Stats;
using UnityEngine;

namespace Data.Item
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Data/Item/Weapon", order = 0)]
    public class WeaponData : ItemData
    {
        public WeaponStats[] weaponStatsList;
        //TODO: Remove
        public ItemInventory weaponInventory;
        public GameObject weaponPrefab;

        public override string GetItemDesc()
        {
            StringBuilder sb = new StringBuilder(itemDescription);
            sb.Replace("{DAMAGES}", "");
            sb.Replace("{COUNT}", "");
            return sb.ToString();
        }

        public override void OnItemSelected()
        {
            int itemLevel = 0;
            foreach (var item in weaponInventory.itemDataList)
            {
                if (item.itemData.itemId == itemId)
                {
                    itemLevel = item.itemLevel;
                    //TODO:Replace gameobject.trygetcomponent as certain class                    
                    if (item.itemInstance.TryGetComponent<ProjectileWeapon>(out var w))
                    {
                        w.LevelUp(weaponStatsList[itemLevel]);
                        item.itemLevel++;
                    }
                    else
                    {
                        Debug.Log("No PJ Weapon");
                    }
                }
            }

            if (weaponInventory.itemDataList.Count == weaponInventory.maxItemCount) return;
            if (itemLevel == 0)
            {
                var item = new InventoryItem
                {
                    itemData = this,
                    itemLevel = 1,
                };
                var go = Instantiate(weaponPrefab, GameManager.Instance.playerTransform);
                item.itemInstance = (GameObject) go;
                if (item.itemInstance.TryGetComponent<ProjectileWeapon>(out var w))
                {
                    w.Initialize(weaponStatsList[itemLevel]);
                }

                weaponInventory.AddItem(item);
            }
        }
    }
}