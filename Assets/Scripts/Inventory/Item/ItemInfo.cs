
using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ItemInfo", menuName = "ItemInfo")]
public class ItemInfo : ScriptableObject, IItemInfo
{
    [SerializeField] protected string _name;
    [SerializeField] protected Sprite _icon;
    [SerializeField] protected int _maxQuantity;
    [SerializeField] protected ItemType _type;
    [SerializeField] protected FuckingType _fuckingType = FuckingType.None;
    [SerializeField] protected Throw _throw;
    [SerializeField] protected int _damage;
    [SerializeField] private UsableEffect _posionEffect;
    public UsableEffect UsableEffect => _posionEffect;
    public int Damage => _damage;
    public string Name => _name;
    public Sprite Icon => _icon;
    public ItemType Type => _type;
    public int MaxQuantity => _maxQuantity;
    public Throw Throw => _throw;
    public FuckingType FuckingType => _fuckingType;
}

public enum FuckingType
{
    None,
    Damaging,
    Usable,
}