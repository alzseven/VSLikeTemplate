using Data.Variable;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    //TODO: UISlider?
    [RequireComponent(typeof(Slider))]
    public class UIExpBar : MonoBehaviour
    {
        [SerializeField] private StatFloatWithMax exp;
        private Slider _expSlider;

        private void Awake() => _expSlider = GetComponent<Slider>();

        private void OnEnable()
        {
            if (exp != null) exp.OnValueChanged += OnExpChanged;
        }

        private void OnDisable()
        {
            if (exp != null) exp.OnValueChanged -= OnExpChanged;
        }
        
        private void OnExpChanged(float value) => _expSlider.value = value / exp.GetMaxValue;
    }
}