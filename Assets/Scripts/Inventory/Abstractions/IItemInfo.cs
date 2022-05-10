using System;
using UnityEngine;

public interface IItemInfo
{   
    UsableEffect UsableEffect { get; }
    int Damage { get; }
    string Name { get; }
    Sprite Icon { get; }
    ItemType Type { get; }
    int MaxQuantity { get; }
    Throw Throw { get; }
    FuckingType FuckingType { get; }
    
}