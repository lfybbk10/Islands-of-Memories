using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Ghost : MonoBehaviour, IDamageable
{
    [SerializeField] private Flicker _flicker;
    [SerializeField] private float _health;
    [SerializeField] private Animator _animator;
    private NavMeshAgent _agent;
    public Action Dead;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        Dead += OnDead;
    }

    private void FixedUpdate()
    {
        _agent.SetDestination(RuntimeContext.Instance.hero.transform.position);
        _animator.SetFloat("Forward", 0.9f, 0.1f, Time.deltaTime);
    }


    private void OnDead()
    {
        
    }

    public float Health
    {
        get => _health;
        set
        {
            Dead?.Invoke();
            _health = value;
        }
    }

    public void ApplyDamage(float value)
    {
        Health -= value;
        _flicker.Create();
    }

    private void OnDestroy()
    {
        Dead -= OnDead;
    }
}
