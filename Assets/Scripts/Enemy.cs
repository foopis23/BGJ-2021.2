using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using CallbackEvents;

public class Enemy : LivingEntity
{
    // editor fields
    public float attackDelay;
    public float attackRange;
    public float attackDamage;

    // private fields
    private NavMeshAgent _navMeshAgent;
    private GameObject _attackTarget;

    // public properties
    public GameObject aggroTarget { get; set; }

    private void Start()
    {
        InitEvent();
        Heal(MaxHealth);
        aggroTarget = null;

        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (!IsAlive) return;
        if (aggroTarget == null) return;
        
        _navMeshAgent.SetDestination(aggroTarget.transform.position);

        if(Vector3.Distance(transform.position, aggroTarget.transform.position) < attackRange)
        {
            // TODO: play attack animation
            _attackTarget = aggroTarget;
            EventSystem.Current.CallbackAfter(Attack, (int) (attackDelay * 1000));
        }
    }

    protected override void OnDeath()
    {
        _navMeshAgent.enabled = false;
    }

    private void Attack()
    {
        if (!IsAlive) return;
        if (_attackTarget == null ||
            !(Vector3.Distance(transform.position, _attackTarget.transform.position) < attackRange)) return;
        
        var livingEntity = _attackTarget.GetComponent<LivingEntity>();
        if(livingEntity != null)
        {
            livingEntity.Damage(attackDamage);
            _attackTarget = null;
        }
    }
}