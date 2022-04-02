using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
public class UIItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Transform _cameraTransform;
    private CanvasGroup _canvasGroup;
    private Canvas _canvas;
    private RectTransform _rectTransform;
    private Transform _inventoryTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var parent = _rectTransform.parent;
        parent.SetAsLastSibling();
        _canvasGroup.blocksRaycasts = false;
        _inventoryTransform = parent.parent.transform;
        parent.SetParent(_canvas.transform);
    }
    
    public void OnDrag(PointerEventData eventData) => 
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;


    private bool In(RectTransform originalParent)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(originalParent, transform.position);
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        // if (!In((RectTransform) _inventoryTransform))
        // {
        //     var uiSlot = GetComponentInParent<UIInventorySlot>();
        //     var uiInvetory = GetComponentInParent<UIInventory>();
        //     //TODO Переименовать _uiSlots в UISlots
        //     var item = uiSlot.Slot.Item;
        //     for (int i = 0; i < item.State.Amount; i++)
        //     {
        //         var obj = Instantiate(item.Info.Prefab);  
        //         //TODO Изменить куда спавнится предмет.
        //         obj.transform.position = Vector3.forward;
        //     }
        //     //uiInvetory.Inventory.RemoveFromSlot(item.Info.Type,uiSlot.Slot,item.State.Amount);
        // }

        var parent = _rectTransform.parent;
        parent.parent = _inventoryTransform;
        parent.SetAsFirstSibling();
        _rectTransform.localPosition = new Vector3(0, 0, 0);
        _canvasGroup.blocksRaycasts = true;
    }
}