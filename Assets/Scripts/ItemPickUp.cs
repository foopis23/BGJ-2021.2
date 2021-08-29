using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public int health;
    public int coins;
    public GameObject wrapper;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("HELLO");
        if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;
        
        Debug.Log("world");
        
        var player = other.GetComponent<Player>();

        if (health > 0)
        {
            player.Heal(health);
        }

        if (coins > 0)
        {
            player.purchaseCardPoints += coins;
        }
        
        Destroy(wrapper);
    }
}
