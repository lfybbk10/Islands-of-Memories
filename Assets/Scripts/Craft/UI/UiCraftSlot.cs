using System;
using UnityEngine;
using UnityEngine.UI;

namespace Craft
{
    public class UiCraftSlot : MonoBehaviour
    {
        [SerializeField] private Recipe _recipe;
        [SerializeField] private Button _slot;
        [SerializeField] private Image _image;
        [SerializeField] private Text _text;
        private Action<IRecipe> _clicked;

        private void Awake()
        {
            _slot.onClick.AddListener(() => { _clicked?.Invoke(_recipe); });
            _image.sprite = _recipe.Received.Info.Icon;
            _text.text = $"x{_recipe.Received.Amount}";
        }

        public void Init(Action<IRecipe> clicked)
        {
            _clicked = clicked;
        }
    }
}