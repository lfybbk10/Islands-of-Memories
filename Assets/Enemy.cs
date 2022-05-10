using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private Flicker _flicker;
    [SerializeField] private float _health;
    [SerializeField] private LayerMask _heroMask;
    private Animator _animator;
    private NavMeshAgent _agent;
    private Collider[] _colliders = new Collider[50];
    public Action Dead { get; set; }

    [Header("Combat")] 
    [SerializeField] private float _attack = 3f;
    [SerializeField] private float _range = 1f;
    [SerializeField] private float _aggroRange = 4f;

    private float _timePassed;
    private Hero _hero;

    private void Awake()
    {
        
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        Dead += OnDead;
    }

    private void Start()
    {
        _hero = RuntimeContext.Instance.hero;
    }

    private void OnDead()
    {
    }

    public float Value
    {
        get => _health;
        set => _health = value;
    }

    public void TakeDamage()
    {
        var count = Physics.OverlapSphereNonAlloc(transform.position, 10f, _colliders, _heroMask);
        if (count == 0)
            return;
        _colliders[0].GetComponent<IDamageable>().ApplyDamage(1);
    }


    private void Update()
    {
        _animator.SetFloat("Speed", _agent.velocity.magnitude / _agent.speed);
        if (_timePassed >= _attack)
        {
            if (Vector3.Distance(_hero.transform.position, transform.position) <= _range)
            {
                _animator.SetTrigger("Attack");
                _timePassed = 0;
            }
        }
        _timePassed += Time.deltaTime;
        if (Vector3.Distance(_hero.transform.position, transform.position) <= _aggroRange)
        {
            _agent.SetDestination(_hero.transform.position);
        }
        transform.LookAt(_hero.transform);
    }


    public void ApplyDamage(float value)
    {
        Value -= value;
        if (Value < 0)
            Destroy(gameObject);
        _animator.SetTrigger("Damage");
        //_flicker.Create();
    }

    private void OnDestroy()
    {
        Dead -= OnDead;
    }

    private void OnDrawGizmos()
    {   
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,_range);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,_aggroRange);
        
    }
}