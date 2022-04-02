using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectHandler : MonoBehaviour
{
    [SerializeField] private GameObject _prompts;
    [SerializeField] private UIInventory _inventory;
    [SerializeField] private CampFire _campFire;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _player;
    private readonly Collider[] _findedColliders = new Collider[50];
    [SerializeField] private GameObject _text;

    private void FixedUpdate()
    {
        var count = Physics.OverlapSphereNonAlloc(_player.position, 6f, _findedColliders, _layerMask);
        if (count > 0)
        {
            var nearlesObj = _findedColliders[0];
            _text.transform.position = new Vector3(nearlesObj.transform.position.x,nearlesObj.transform.position.y + 1f, nearlesObj.transform.position.z);
            _text.transform.DOScale(new Vector3(-1, 1, 1), 0.15f);
            // if (Input.GetKeyDown(KeyCode.E))
            // {
            //     var dropItem = nearlesObj.GetComponent<Item>();
            //      _inventory.Add(new Item(dropItem.ItemInfo,1));
            //     var parent = nearlesObj.transform.parent;
            //     if (parent != null)
            //         Destroy(parent.gameObject);
            //     _text.transform.DOScale(new Vector3(0, 0, 0), 0.15f);
            //     Destroy(nearlesObj.transform.gameObject);
            // }
        }
        else _text.transform.DOScale(new Vector3(0, 0, 0), 0.15f);
    }
}