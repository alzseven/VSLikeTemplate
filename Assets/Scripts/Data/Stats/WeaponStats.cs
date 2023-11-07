using UnityEngine;

namespace Data.Stats
{
    [CreateAssetMenu(fileName = "WeaponStats", menuName = "Data/Stats/Weapon", order = 11)]
    public class WeaponStats : ScriptableObject
    {
        public float weaponDamage;
        public float weaponAreaSize;
        public float weaponDuration;
        public float weaponSpeed;
        public float weaponCoolDown;
        public int weaponAmount;
        public int weaponPierce;
    }
}
