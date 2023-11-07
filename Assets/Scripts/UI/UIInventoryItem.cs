using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIInventoryItem : MonoBehaviour
    {
        private Image _icon;
        private TMP_Text _textLevel;

        private void Awake()
        {
            _icon = GetComponentsInChildren<Image>()[1];
            
            TMP_Text text = GetComponentInChildren<TMP_Text>();
            if (text) _textLevel = text;
        }


        public void UpdateItemDisplay(Sprite icon, int i)
        {
            _icon.sprite = icon;
            if (_textLevel) _textLevel.text = $"Lv.{i}";
        }
    }
}