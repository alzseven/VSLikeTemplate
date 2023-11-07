using Data.Item;
using Data.Variable;
using UnityEngine;
using UnityEngine.Events;

namespace Contents.Player
{
    public class PlayerStats : MonoBehaviour
    {
        public int level;
        public StatFloatWithMax health;
        public StatFloatWithMax exp;
        public ItemInventory inventory;
        public WeaponData defaultWeaponData;
        // TODO: Any PowerUps?

        // TODO:
        public UnityEvent onPlayerLevelUp;
        
        private void Start()
        {
            health.Value = health.GetMaxValue;
            defaultWeaponData.OnItemSelected();
        }

        private void OnEnable()
        {
            exp.OnValueChanged += OnExpChanged;
            health.OnValueChanged += OnHpChanged;
        }

        private void OnDisable()
        {
            exp.OnValueChanged -= OnExpChanged;
            health.OnValueChanged -= OnHpChanged;
        }

        private void OnHpChanged(float value)
        {
            if (value <= 0f)
            {
                //PlayerDead!
                //TODO: GameManager.GamerOver;
                Time.timeScale = 0f;
                Debug.Log("GameOver");
            }
        }
        
        private void OnExpChanged(float value)
        {
            if (value >= exp.GetMaxValue)
            {
                level++;
                onPlayerLevelUp?.Invoke();
            }
        }
        
    }
}
