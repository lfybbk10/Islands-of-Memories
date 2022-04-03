using System;
using UnityEngine;

namespace Craft
{
    public class UICraft : MonoBehaviour
    {
        [SerializeField] private UICraftMenu _craftMenu;
        [SerializeField] private UiCraftSlot[] _slots;
        [SerializeField] private CraftHandler _craftHandler;
        private Action<IRecipe> _slotClicked;
        private Action _crafted;
        private IRecipe _recipe;
        private void Awake()
        {
            _slotClicked += OnSlotClicked;
            _crafted += OnCrafted;
            _craftMenu.Init(_crafted);
            foreach (var slot in _slots)
                slot.Init(_slotClicked);
        }
        
        private void OnCrafted() => _craftHandler.Craft(_recipe);

        private void OnSlotClicked(IRecipe recipe)
        {
            _recipe = recipe;
            _craftMenu.Replace(recipe);
        }
        
    }
}