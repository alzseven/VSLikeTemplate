using Data.Variable;
using TMPro;
using UnityEngine;

namespace UI
{
    //TODO: UI__Text?
    [RequireComponent(typeof(TMP_Text))]
    public class UILevelText : MonoBehaviour
    {
        [SerializeField] private StatFloatWithMax level;
        private TMP_Text _levelText;

        private void Awake() => _levelText = GetComponent<TMP_Text>();

        private void OnEnable() => level.OnValueChanged += OnLevelChanged;

        private void OnDisable() => level.OnValueChanged -= OnLevelChanged;

        private void OnLevelChanged(float newLevel) => _levelText.text = $"LV.{newLevel}";
    }
}