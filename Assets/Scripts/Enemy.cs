using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LivingEntity
{
    // editor fields
    public LayerMask ViewLayerMask;

    // private fields
    private NavMeshAgent navMeshAgent;
    private SphereCollider sphereCollider;
    private GameObject aggroTarget;

    void Start()
    {
        Heal(MaxHealth);
        aggroTarget = null;

        navMeshAgent = GetComponent<NavMeshAgent>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    void Update()
    {
        if(aggroTarget != null)
        {
            navMeshAgent.SetDestination(aggroTarget.transform.position);
        }
    }

    protected override void OnDeath()
    {
        navMeshAgent.enabled = false;
    }

    void OnTriggerStay(Collider col)
    {
        if(col.tag == "Player" && col.gameObject != aggroTarget && (aggroTarget == null || Vector3.Distance(transform.position, col.gameObject.transform.position) < Vector3.Distance(transform.position, aggroTarget.transform.position)))
        {
            RaycastHit hit;
            Physics.Raycast(transform.TransformPoint(sphereCollider.center), col.gameObject.transform.position - transform.TransformPoint(sphereCollider.center), out hit, sphereCollider.radius + 1);
            if(hit.collider == col)
            {
                aggroTarget = col.gameObject;
            }
        }
    }
}
