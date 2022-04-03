using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

namespace Craft
{
    public class UICraftMenu : MonoBehaviour
    {
        [SerializeField] private Image _main;
        [SerializeField] private Text _text;
        [SerializeField] private Button _craft;
        [SerializeField] private Text _amount;
        [SerializeField] private UIRequirementSlot _uiRequirementSlot;
        [SerializeField] private HorizontalLayoutGroup _horizontal;
        [SerializeField] private UIInventory _uiInventory;
        private const string AMOUNT = "x{0}";
        private List<UIRequirementSlot> _slots;
        private Action _crafted;
        private IRecipe _recipe;
        private Inventory _inventory;

        private void Start()
        {
            _slots = new List<UIRequirementSlot>();
            _main.enabled = false;
            _amount.enabled = false;
            _craft.onClick.AddListener(() => { _crafted?.Invoke();});
            _inventory = _uiInventory.GetInventory();
        }
        public void Init(Action crafted)
        {
            _crafted = crafted;
        }
        public void Replace(IRecipe recipe)
        {
            if (recipe == _recipe)
                return;
            _recipe = recipe;
            if (_slots.Count != 0)
            {
                for (int i = _slots.Count - 1; i >= 0; i--)
                {
                    var slot = _slots[i];
                    slot.transform.DOScale(new Vector3(0,0,0),0.15f).OnComplete( ()=>
                    {
                        Destroy(slot.gameObject);
                    });
                }
                _slots.Clear();
            }
            _text.DOText(recipe.Received.Info.Name,0.2f);
            _main.transform.DOScale(new Vector3(0, 0, 0), 0.15f).OnComplete(() =>
            {
                _main.enabled = true;
                _amount.enabled = true;
                _amount.text = string.Format(AMOUNT, recipe.Received.Amount);
                _main.sprite = recipe.Received.Info.Icon;
                _main.transform.DOScale(new Vector3(1, 1, 1), 0.15f);
            });
            foreach (var component in recipe.Requirements)
            {
                var slot = Instantiate(_uiRequirementSlot, _horizontal.transform);
                slot.Init(_inventory);
                slot.Set(component);
                slot.transform.localScale = new Vector3(0, 0, 0);
                _slots.Add(slot);
                slot.transform.DOScale(new Vector3(1, 1, 1), 0.15f).SetDelay(.2f);
            }
        }
    }
}