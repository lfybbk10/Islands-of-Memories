using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private Flicker _flicker;
    [SerializeField] private float _health;
    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _hero;
    private NavMeshAgent _agent;
    private Collider[] _colliders = new Collider[50];
    public Action Dead { get; set; }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        Dead += OnDead;
    }

    private void FixedUpdate()
    {
        // _agent.SetDestination(RuntimeContext.Instance.hero.transform.position);
        // _animator.SetFloat("Forward", 0.9f, 0.1f, Time.deltaTime);
    }

    private void OnDead()
    {
        
    }

    public float Value
    {
        get => _health;
        set
        {
            Dead?.Invoke();
            _health = value;
        }
    }

    public void TakeDamage()
    {
        var count = Physics.OverlapSphereNonAlloc(transform.position, 10f, _colliders,_hero);
        if (count == 0)
            return;
        _colliders[0].GetComponent<IDamageable>().ApplyDamage(1);
    }
    
    
    public void ApplyDamage(float value)
    {
        Value -= value;
        _flicker.Create();
    }

    private void OnDestroy()
    {
        Dead -= OnDead;
    }
}
