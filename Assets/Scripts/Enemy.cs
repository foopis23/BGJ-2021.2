using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using CallbackEvents;
using UnityEngine.Serialization;

public class Enemy : LivingEntity
{
    // editor fields
    public float attackCooldown;
    public float attackDamageDelay;
    public float attackRange;
    public float attackDamage;
    [FormerlySerializedAs("Animator")] public Animator animator;
    public Collider collider;
    public int corpseStayingPower = 30;
    
    // private fields
    private NavMeshAgent _navMeshAgent;
    private GameObject _attackTarget;
    private bool isAttacking;

    // public properties
    public GameObject aggroTarget { get; set; }

    private void Start()
    {
        InitEvent();
        Heal(MaxHealth);
        aggroTarget = null;
        isAttacking = false;

        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (!IsAlive) return;
        if (aggroTarget == null) return;

        _navMeshAgent.SetDestination(aggroTarget.transform.position);

        if (animator.speed != 0)
        {
            animator.Play("walk");
        }

        if (Vector3.Distance(transform.position, aggroTarget.transform.position) < attackRange && !isAttacking) {
            animator.Play("attack");
            isAttacking = true;
            _attackTarget = aggroTarget;
            EventSystem.Current.CallbackAfter(Attack, (int) (attackDamageDelay * 1000));
        }
    }

    protected override void OnDeath()
    {
        _navMeshAgent.enabled = false;
        animator.Play("death");
        collider.enabled = false;
        
        EventSystem.Current.CallbackAfter(() =>
        {
            Destroy(gameObject);
        }, corpseStayingPower * 1000);
    }

    private void Attack()
    {
        EventSystem.Current.CallbackAfter(() => 
        {
            isAttacking = false;
        }, (int) ((attackCooldown - attackDamageDelay) * 1000));

        if (!IsAlive || _attackTarget == null || !(Vector3.Distance(transform.position, _attackTarget.transform.position) < attackRange)) return;
        
        var livingEntity = _attackTarget.GetComponent<LivingEntity>();
        
        if(livingEntity != null)
        {
            livingEntity.Damage(attackDamage);
            _attackTarget = null;
        }
    }
}