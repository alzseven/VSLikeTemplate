using Data.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class UILevelUpButton : MonoBehaviour
    {
        public ItemData itemData;
        [SerializeField] private Sprite defaultSprite;

        private Button _itemSelectButton;
        
        private Image _icon;
        private TMP_Text _textLevel;
        private TMP_Text _textName;
        private TMP_Text _textDesc;
        private ItemData _targetItemData;

        private void Awake()
        {
            _icon = GetComponentsInChildren<Image>()[1];
            

            TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
            _textLevel = texts[0];
            _textName = texts[1];
            _textDesc = texts[2];

            _itemSelectButton = GetComponent<Button>();
        }

        private void OnEnable() => _itemSelectButton.onClick.AddListener(OnClick);

        private void OnDisable() => _itemSelectButton.onClick.RemoveListener(OnClick);

        public void Initialize(ItemData data, int level = 0)
        {
            itemData = data;
            
            _icon.sprite = itemData.itemIcon;
            _textLevel.text = level + 1 > 1 ? $"Lv.{level + 1}" : "New";
            _textName.text = itemData.itemName;
            _textDesc.text = itemData.itemDescription;
        }
        
        private void OnClick()
        {
            if(itemData) itemData.OnItemSelected();
        }

        public void Clear()
        {
            itemData = null;
            
            _icon.sprite = defaultSprite;
            _textLevel.text = "";
            _textName.text = "";
            _textDesc.text = "";
        }
    }
}