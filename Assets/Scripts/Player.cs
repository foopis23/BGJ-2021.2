using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CallbackEvents;

public class Player : MonoBehaviour
{
    public float Health;
    public float MaxHealth;
    public bool IsAlive = true;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Damage(float damage)
    {
        Health -= damage;
        if(Health <= 0f)
        {
            Health = 0f;
            IsAlive = false;
        }
    }
}
