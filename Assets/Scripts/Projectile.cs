using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CallbackEvents;

public class Projectile : MonoBehaviour
{
    // Editor Fields
    public float Damage;
    public float Speed;
    public float MinSpeed = 1;
    public float Range;
    public float MinRange = 10;
    public int Bounces;
    public int MinBounces = 0;
    public int Pierces;
    public int MinPierces = 0;
    public bool IsGrapeShot = false;

    // Private Fields
    private float distanceTraveled;
    private int bouncesLeft;
    private int piercesLeft;

    void Start()
    {
        EventSystem.Current.FireEvent(new OnFireContext {Projectile = this});
        distanceTraveled = 0;
        bouncesLeft = Bounces;
        piercesLeft = Pierces;
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        if(Range < MinRange) { Range = MinRange; }
        if(Speed < MinSpeed) { Speed = MinSpeed; }
        if(Bounces < MinBounces) { Bounces = MinBounces; }
        if(Pierces < MinPierces) { Pierces = MinPierces; }

        float targetDistance = Speed * Time.fixedDeltaTime;
        bool hitSuccess;
        do
        {
            RaycastHit hit;
            hitSuccess = Physics.Raycast(transform.position, transform.forward, out hit, targetDistance) && hit.distance < Range - distanceTraveled;
            if(hitSuccess)
            {
                switch(hit.collider.tag)
                {
                    case "Level":
                        transform.forward = Vector3.Reflect(transform.forward, hit.normal);
                        EventSystem.Current.FireEvent(new OnHitWallContext {Projectile = this, Normal = hit.normal});
                        if(--bouncesLeft == 0)
                        {
                            Expire(true);
                            return;
                        }

                        break;

                    case "Enemy":
                        EventSystem.Current.FireEvent(new OnHitEnemyContext {Projectile = this, Enemy = hit.collider.gameObject.GetComponent<Enemy>()});
                        // TODO: damage enemy (once enemy implemented)
                        // TODO (also once enemy implemented): check to make sure we dont accidentally apply this twice
                        if(--piercesLeft == 0)
                        {
                            Expire(true);
                            return;
                        }

                        break;

                    case "Player":
                        EventSystem.Current.FireEvent(new OnHitPlayerContext {Projectile = this Player = hit.collider.gameObject.GetComponent<Player>()});
                        // TODO: damage player (once player implemented)
                        // TODO: also the thing with not hitging twihuchceceeeaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                        if(--piercesLeft == 0)
                        {
                            Expire(true);
                            return;
                        }

                        break;
                }

                transform.position += transform.forward * hit.distance;
                distanceTraveled += hit.distance;
                targetDistance -= hit.distance;
            }
        }
        while(hitSuccess);

        if(targetDistance > Range - distanceTraveled)
        {
            Expire(false);
        }
        else
        {
            transform.position += transform.forward * targetDistance;
            distanceTraveled += targetDistance;
        }
    }

    private void Expire(bool onHit)
    {
        EventSystem.Current.FireEvent(new OnExpireContext {Projectile = this, ExpiredOnHit = onHit});
        Destroy(this.gameObject);
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
    public Projectile Projectile;
    public bool ExpiredOnHit;
}
