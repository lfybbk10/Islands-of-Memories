using System;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField] private Point _point;
    
    public void Drop(Inventory inventory, UIInventorySlot uiSlot)
    {
        inventory.RemoveFromSlot(uiSlot.Slot);
    }
}