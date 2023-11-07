using Data.Variable;
using TMPro;
using UnityEngine;

namespace UI
{
    //TODO: UI__Text?
    [RequireComponent(typeof(TMP_Text))]
    public class UIKillCountText : MonoBehaviour
    {
        [SerializeField] private StatFloatWithMax killCount;
        private TMP_Text _levelText;

        private void Awake() => _levelText = GetComponent<TMP_Text>();

        private void OnEnable() => killCount.OnValueChanged += OnKillCountChanged;

        private void OnDisable() => killCount.OnValueChanged -= OnKillCountChanged;

        private void OnKillCountChanged(float newKillCount) => _levelText.text = $"{newKillCount}";
    }
}