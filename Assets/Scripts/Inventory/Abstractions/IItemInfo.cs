    
    using System;
    using UnityEngine;
    
    public interface IItemInfo
    {
         string Name { get; }
         Sprite Icon { get; }
         ItemType Type { get; }
         int MaxQuantity { get; }
         GameObject Prefab { get; }
    }