using System;
using System.Collections.Generic;
using UnityEngine;
using CallbackEvents;
using Exceptions;
using UnityEngine.Serialization;

public class Projectile : MonoBehaviour
{
    // Editor Fields
    [FormerlySerializedAs("Damage")] public float damage;
    [FormerlySerializedAs("Speed")] public float speed;
    [FormerlySerializedAs("MinSpeed")] public float minSpeed = 1;
    [FormerlySerializedAs("Range")] public float range;
    [FormerlySerializedAs("MinRange")] public float minRange = 10;
    [FormerlySerializedAs("Bounces")] public int bounces;
    [FormerlySerializedAs("MinBounces")] public int minBounces;
    [FormerlySerializedAs("Pierces")] public int pierces;
    [FormerlySerializedAs("MinPierces")] public int minPierces;
    // [FormerlySerializedAs("IsGrapeShot")] public bool isGrapeShot = false;
    
    public LayerMask projectileLayerMask;
    public int baseExplosionPower = 0;

    public GameObject LivingHitParticlePrefab;
    public GameObject NonLivingHitParticlePrefab;
    
     // used for modifier
    [NonSerialized] public LivingEntity Dad;

    public List<LivingEntity> AllHitEntities { get; private set; }

    // Private Fields
    private float _distanceTraveled;
    private int _totalBounces;
    private int _totalPierces;
    private HashSet<int> _hitEnemies;
    private ParticleSystem _trailParticle;

    private void Start()
    {
        EventSystem.Current.FireEvent(new OnFireContext {Projectile = this});
        _distanceTraveled = 0;
        _totalBounces = 0;
        _totalPierces = 0;
        AllHitEntities = new List<LivingEntity>();

        _hitEnemies ??= new HashSet<int>();

        _trailParticle = GetComponentInChildren<ParticleSystem>();
        var main = _trailParticle.main;
        main.startSpeed = speed + 1;
        _trailParticle.Play();
    }

    private void FixedUpdate()
    {
        if(range < minRange) { range = minRange; }
        if(speed < minSpeed) { speed = minSpeed; }
        if(bounces < minBounces) { bounces = minBounces; }
        if(pierces < minPierces) { pierces = minPierces; }

        _hitEnemies ??= new HashSet<int>();
        var projectileTransform = transform;

        var targetDistance = speed * Time.fixedDeltaTime;
        bool hitSuccess;
        do
        {
            hitSuccess = Physics.Raycast(projectileTransform.position, projectileTransform.forward, out var hit, targetDistance, projectileLayerMask) && hit.distance < range - _distanceTraveled;
            if (!hitSuccess) continue;
            
            var hitObject = hit.collider.gameObject;
            if(hitObject.layer == LayerMask.NameToLayer("Level"))
            {
                projectileTransform.forward = Vector3.Reflect(projectileTransform.forward, hit.normal);
                EventSystem.Current.FireEvent(new OnHitWallContext {Projectile = this, Normal = hit.normal});
                
                if(_totalBounces++ >= bounces)
                {
                    Expire(true);
                    var particleObject = Instantiate(NonLivingHitParticlePrefab);
                    particleObject.transform.position = hit.point;
                    particleObject.transform.forward = hit.normal;
                    return;
                }
                
                // Clear hit set so that after bounce enemy could be hit again
                _hitEnemies.Clear();
            }
            else if(hitObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                var entity = hit.collider.gameObject.GetComponent<Enemy>();
                EventSystem.Current.FireEvent(new OnHitEnemyContext {Projectile = this, Enemy = entity});
                    
                DamageEntity(hit.collider.gameObject);
                AllHitEntities.Add(entity);

                if(_totalPierces++ >= pierces)
                {
                    Expire(true);
                    var particleObject = Instantiate(LivingHitParticlePrefab);
                    particleObject.transform.position = hit.point;
                    particleObject.transform.forward = hit.normal;
                    return;
                }
            }
            else if(hitObject.layer == LayerMask.NameToLayer("Player"))
            {
                var entity = hit.collider.gameObject.GetComponent<Player>();
                EventSystem.Current.FireEvent(new OnHitPlayerContext {Projectile = this, Player = entity});
                    
                DamageEntity(hit.collider.gameObject);
                AllHitEntities.Add(entity);

                if(_totalPierces++ >= pierces)
                {
                    Expire(true);
                    var particleObject = Instantiate(LivingHitParticlePrefab);
                    particleObject.transform.position = hit.point;
                    particleObject.transform.forward = hit.normal;
                    return;
                }
            }
            else
            {
                throw new Exception("If you reach this, it means your layer mask is including to many layers. ahaha");
            }
            
            projectileTransform.position += projectileTransform.forward * hit.distance;
            _distanceTraveled += hit.distance;
            targetDistance -= hit.distance;
        }
        while(hitSuccess);

        if(targetDistance > range - _distanceTraveled)
        {
            Expire(false);
        }
        else
        {
            projectileTransform.position += projectileTransform.forward * targetDistance;
            _distanceTraveled += targetDistance;
        }
    }

    private void DamageEntity(GameObject o)
    {
        // check if object was already hit 
        if (_hitEnemies.Contains(o.GetInstanceID())) return;

        // get the entity of the object
        var entity = o.GetComponent<LivingEntity>();
        if (entity == null) throw new MissingEntityException(o);
        
        // Apply Damage
        entity.Damage(damage);
        
        // Add Objcet to damaged set
        _hitEnemies.Add(o.GetInstanceID());
    }

    private void Expire(bool onHit)
    {
        var expireCtx = EventSystem.Current.FireFilter<OnExpireContext>(new OnExpireContext(this, onHit) {ExplosionPower = baseExplosionPower});
        var explosionPower = Mathf.Max(expireCtx.ExplosionPower, 0);

        if (explosionPower > 0) {
            EventSystem.Current.FireEvent(new ExplosionEventContext(explosionPower){Pos = transform.position});
        }
        
        Destroy(gameObject);
    }
}

public class OnFireContext : EventContext
{
    public Projectile Projectile;
}

public class OnHitWallContext : EventContext
{
    public Projectile Projectile;
    public Vector3 Normal;
}

public class OnHitEnemyContext : EventContext
{
    public Projectile Projectile;
    public Enemy Enemy;
}

public class OnHitPlayerContext : EventContext
{
    public Projectile Projectile;
    public Player Player; 
}

public class OnExpireContext : EventContext
{
    public readonly Projectile Projectile;
    public readonly bool ExpiredOnHit;
    public int ExplosionPower;

    public OnExpireContext(Projectile projectile, bool expiredOnHit)
    {
        Projectile = projectile;
        ExpiredOnHit = expiredOnHit;
    }
}

public class ExplosionEventContext : EventContext
{
    public float Range;
    public float Damage;
    public Vector3 Pos;
    public int PowerLevel;

    public ExplosionEventContext(int powerLevel)
    {
        const float damageMultiplier = 20.0f;
        const float rangeMultiplier = 3.0f;
        
        PowerLevel = powerLevel;
        Damage = powerLevel * damageMultiplier;
        Range = powerLevel * rangeMultiplier;
    }

    public float GetDamage(Vector3 entityPos)
    {
        var distance = Vector3.Distance(Pos, entityPos);
        if (distance >= Range) return 0.0f;
        var rangeRt = Mathf.Sqrt(Range);
        var distanceRt = Mathf.Sqrt(distance);
        return ((rangeRt - distanceRt) / (rangeRt + distanceRt)) * Damage;
    }
}