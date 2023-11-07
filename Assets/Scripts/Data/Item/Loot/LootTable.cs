using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Data.Item.Loot
{
    [CreateAssetMenu(fileName = "ItemLootTable", menuName = "Data/Item/LootTable", order = 0)]
    public class LootTable : ScriptableObject
    {
        public LootData[] loot;

        private float TotalDropChance()
        {
            var res = 0f;
            foreach (var data in loot) res += data.dropChance;
            return res;
        }

        public ItemData GetRandomWeightedSingleItem()
        {
            var rand = Random.Range(0, TotalDropChance());

            var currentRangeMin = 0f;
            
            foreach (var data in loot)
            {
                if (rand > currentRangeMin && rand <= (currentRangeMin + data.dropChance))
                {
                    return data.lootableItem;
                }

                currentRangeMin += data.dropChance;
            }

            //TODO:
            //Something Wrong;
            return null;
        }
        
        public ItemData[] GetRandomWeightedUniqueItems(int count)
        {
            var res = new List<ItemData>();

            if (loot.Length < count)
            {
                foreach (var lootData in loot)
                {
                    res.Add(lootData.lootableItem);
                } 
            }
            else
            {
                while (res.Count < count)
                {
                    var rand = Random.Range(0, TotalDropChance());

                    var currentRangeMin = 0f;
            
                    foreach (var data in loot)
                    {
                        if (rand > currentRangeMin && rand <= (currentRangeMin + data.dropChance))
                        {
                            if(res.Contains(data.lootableItem) == false)
                                res.Add(data.lootableItem);
                        }
                        currentRangeMin += data.dropChance;
                    }
                }
            }
            return res.ToArray();
        }
    }
}