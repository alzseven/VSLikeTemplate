using Contents.Manager;
using Data.Variable;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    //TODO: inherit UISlider?
    [RequireComponent(typeof(Slider))]
    public class UIHealthBar : MonoBehaviour
    {
        [SerializeField] private StatFloatWithMax health;
        private Slider _hpSlider;
        private RectTransform _rectTransform;
        private Camera _mainCamera;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _hpSlider = GetComponent<Slider>();
        }

        private void OnEnable()
        {
            if (health != null) health.OnValueChanged += OnHealthChanged;
        }

        private void OnDisable()
        {
            if (health != null) health.OnValueChanged -= OnHealthChanged;
        }

        private void Start()
        {
            if (Camera.main is not null)
                _mainCamera = Camera.main;
        }

        private void FixedUpdate()
        {
            if (!ReferenceEquals(_mainCamera, null))
                _rectTransform.position = _mainCamera.WorldToScreenPoint(GameManager.Instance.playerTransform.position);
        }

        private void OnHealthChanged(float value) => _hpSlider.value = value / health.GetMaxValue;
    }
}