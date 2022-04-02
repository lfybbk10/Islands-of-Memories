using System;
using UnityEngine;
using UnityEngine.UI;

namespace Craft
{
    public class UiCraftSlot : MonoBehaviour
    {
        [SerializeField] private Recipe _recipe;
        [SerializeField] private Button _slot;
        private Action<IRecipe> _clicked;

        private void Awake()
        {
            _slot.onClick.AddListener(() => { _clicked?.Invoke(_recipe); });
        }

        public void Init(Action<IRecipe> clicked)
        {
            _clicked = clicked;
        }
    }
}