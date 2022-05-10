using System;
using Infrastructure;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Hero))]
public class HeroControl : MonoBehaviour
{
    private Hero _character;
    private Transform _camera;
    private Vector3 _camForward;
    private Vector3 _move;
    private bool _jump;
    private bool _hit;
    private bool _roll;

    private void Start()
    {
        if (Camera.main != null)
        {
            _camera = Camera.main.transform;
        }

        _character = GetComponent<Hero>();
    }

    private void Update()
    {
        if (Game.IsPaused)
            return;
        if (!_jump)
        {
            _jump = Input.GetKeyDown(KeyCode.Space);
        }

        if (!_hit)
        {
            _hit = Input.GetMouseButtonDown(0);
        }

        if (!_roll)
        {
            _roll = Input.GetKeyDown(KeyCode.LeftShift);
        }
    }

    private void FixedUpdate()
    {
        if (Game.IsPaused)
            return;
        var axis = Game.InputService.Axis;
        var crouch = Input.GetKey(KeyCode.LeftControl);
        var pickedSlot = RuntimeContext.Instance.pickedSlot;
        if (_hit && pickedSlot != null && pickedSlot.Slot.Item.Info.FuckingType == FuckingType.Usable)
            RuntimeContext.Instance.inventory.Use(pickedSlot.Slot);
        pickedSlot = RuntimeContext.Instance.pickedSlot;
        if (pickedSlot == null)
            _hit = false;
        if (pickedSlot != null && pickedSlot.Slot.Item.Info.FuckingType != FuckingType.Damaging)
            _hit = false;
        if (_camera != null)
        {
            _camForward = _camera.forward;
            _camForward = Vector3.Scale(_camForward, new Vector3(1, 0, 1)).normalized;
            _move = axis.y * _camForward + axis.x * _camera.right;
        }
        else _move = axis.y * Vector3.forward + axis.x * Vector3.right;

        _character.Move(_move, crouch, _jump, _hit, _roll);
        _jump = false;
        _hit = false;
        _roll = false;
    }
}