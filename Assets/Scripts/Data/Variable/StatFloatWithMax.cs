using System;
using UnityEngine;

namespace Data.Variable
{
    [CreateAssetMenu(fileName = "StatFloatWithMax", menuName = "Data/Variable/StatFloatWithMax", order = 0)]
    public class StatFloatWithMax : ScriptableObject
    {
        [SerializeField] private float value;
        [SerializeField] private float maxValue;
        public event Action<float> OnValueChanged;

        public float Value
        {
            set
            {
                //TODO: Check value > max
                this.value = value;
                OnValueChanged?.Invoke(this.value);
            }

            get => value;

        }

        public float GetMaxValue => maxValue;

        public void SetMaxValue(float v) => maxValue = v;
    }
}