using Data.Variable;
using TMPro;
using UnityEngine;

namespace UI
{
    //TODO: UI__Text?
    [RequireComponent(typeof(TMP_Text))]
    public class UITimeText : MonoBehaviour
    {
        [SerializeField] private StatFloatWithMax time;
        private TMP_Text _timeText;

        private void Awake() => _timeText = GetComponent<TMP_Text>();

        private void OnEnable() => time.OnValueChanged += OnTimeChanged;

        private void OnDisable() => time.OnValueChanged -= OnTimeChanged;

        private void OnTimeChanged(float newTime) => _timeText.text = $"{(int) (newTime / 60):D2}:{(int) (newTime % 60):D2}";
    }
}