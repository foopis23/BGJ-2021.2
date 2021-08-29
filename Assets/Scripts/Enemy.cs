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

    public GameObject[] itemDropPrefabs;
    public float dropRate = 0.15f;
    public AudioSource attackSound;
    public AudioSource damageSound;
    public AudioSource deathSound;
    
    // private fields
    private NavMeshAgent _navMeshAgent;
    private GameObject _attackTarget;
    private bool isAttacking;

    // public properties
    public GameObject aggroTarget { get; set; }

    private void Start()
    {
        Init();
        
        aggroTarget = null;
        isAttacking = false;

        _navMeshAgent = GetComponent<NavMeshAgent>();
        baseWalkSpeed = _navMeshAgent.speed;
    }

    private void Update()
    {
        _navMeshAgent.speed = WalkSpeed;
        
        if (!IsAlive) return;
        if (aggroTarget == null) return;

        _navMeshAgent.SetDestination(aggroTarget.transform.position);

        if (_navMeshAgent.velocity.magnitude != 0)
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
        base.OnDeath();

        _navMeshAgent.enabled = false;
        DeInit();
        collider.enabled = false;
        
        animator.Play("death");

        deathSound.Play();
        
        EventSystem.Current.CallbackAfter(() =>
        {
            Destroy(gameObject);
        }, corpseStayingPower * 1000);

        if (Random.Range(0.0f, 1.0f) <= dropRate)
        {
            Instantiate(itemDropPrefabs[Random.Range(0, itemDropPrefabs.Length)], transform.position, Quaternion.identity);
        }
    }

    private void Attack()
    {
        EventSystem.Current.CallbackAfter(() => 
        {
            isAttacking = false;
        }, (int) ((attackCooldown - attackDamageDelay) * 1000));

        attackSound.Play();

        if (!IsAlive || _attackTarget == null || !(Vector3.Distance(transform.position, _attackTarget.transform.position) < attackRange)) return;
        
        var livingEntity = _attackTarget.GetComponent<LivingEntity>();
        
        if(livingEntity != null)
        {
            damageSound.Play();
            livingEntity.Damage(attackDamage);
            _attackTarget = null;
        }
    }
}