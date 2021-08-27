using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    // editor fields
    public LayerMask viewLayerMask;

    // private fields
    private Enemy _enemy;
    private SphereCollider _detectionCollider;

    // Start is called before the first frame update
    void Start()
    {
        _enemy = GetComponentInParent<Enemy>();
        _detectionCollider = GetComponentInChildren<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider col)
    {
        if (!_enemy.IsAlive) return;
        if (!col.CompareTag("Player") || col.gameObject == _enemy.aggroTarget || (_enemy.aggroTarget != null &&
            !(Vector3.Distance(transform.position, col.gameObject.transform.position) <
              Vector3.Distance(transform.position, _enemy.aggroTarget.transform.position)))) return;
        
        Physics.Raycast(transform.TransformPoint(_detectionCollider.center), col.gameObject.transform.position - transform.TransformPoint(_detectionCollider.center), out var hit, _detectionCollider.radius + 1);
        if(hit.collider == col)
        {
            _enemy.aggroTarget = col.gameObject;
        }
    }
}
