using System;
using UnityEngine;
using UnityEngine.UI;

namespace Craft
{
    public class UIRequirementSlot : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private Image _image;
        private ItemType _type;
        private int _amount;
        private Inventory _inventory;

        public void Init(Inventory inventory)
        {
            _inventory = inventory;
            _inventory.InventoryChanged += UpgradeText;
        }

        public void Set(Component item)
        {
            _image.sprite = item.Info.Icon;
            _type = item.Info.Type;
            _amount = item.Amount;
            UpgradeText();
        }

        private void UpgradeText()
        {
            var current = _inventory.GetCount(_type);
            if (_text != null)
            {
                if (current < _amount)
                    _text.color = Color.red;
                else _text.color = Color.white;
                _text.text = $"{current}/{_amount}";
            }
        }

        public void OnDestroy()
        {
            _inventory.InventoryChanged -= UpgradeText;
        }
    }
}