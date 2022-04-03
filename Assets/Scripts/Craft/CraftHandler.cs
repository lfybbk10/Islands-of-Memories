using System.Collections.Generic;
using UnityEngine;

namespace Craft
{
    public class CraftHandler : MonoBehaviour
    {
        [SerializeField] private UIInventory _uiInventory;
        private Inventory _inventory => _uiInventory.GetInventory();

        public void Craft(IRecipe recipe)
        {
            if (IsAllowed(recipe.Requirements))
            {
                foreach (var requirement in recipe.Requirements)
                    _inventory.Remove(requirement.Info.Type, requirement.Amount);
                _inventory.Add(new Item(recipe.Received.Info, recipe.Received.Amount));
            }
            else print($"Невозможно создать: {recipe.Received.Info.Name}");
        }
        private bool IsAllowed(IEnumerable<Component> requirements)
        {
            if (_inventory.IsEmpty)
                return false;
            foreach (var requirement in requirements)
            {
                if (!_inventory.Contains(requirement.Info.Type, requirement.Amount))
                    return false;
            }
            return true;
        }
    }
}