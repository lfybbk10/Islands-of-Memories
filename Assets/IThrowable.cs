using UnityEngine;

public interface IThrowable
{
    IItemInfo ItemInfo { get; }
    Rigidbody Rigidbody { get; }
    BoxCollider BoxCollider { get; }
    GameObject Object { get; }
}