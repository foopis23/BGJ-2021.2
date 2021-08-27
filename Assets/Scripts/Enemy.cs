using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LivingEntity
{
    // private fields
    private NavMeshAgent _navMeshAgent;

    // public properties
    public GameObject aggroTarget { get; set; }

    void Start()
    {
        InitEvent();
        Heal(MaxHealth);
        aggroTarget = null;

        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(IsAlive)
        {
            if(aggroTarget != null)
            {
                _navMeshAgent.SetDestination(aggroTarget.transform.position);
            }

            // TODO: attack bois
        }
    }

    protected override void OnDeath()
    {
        _navMeshAgent.enabled = false;
    }
}
