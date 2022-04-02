using System;
using System.Collections.Generic;
using UnityEngine;

namespace Craft
{
    [Serializable]
    [CreateAssetMenu(fileName = "Recipe", menuName = "Recipe")]
    public class Recipe : ScriptableObject, IRecipe
    {
        [SerializeField] private Component _received;
        [SerializeField] private List<Component>  _requirements;
        public Component Received => _received;
        public IEnumerable<Component> Requirements => _requirements;
    }
}