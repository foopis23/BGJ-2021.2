using System.Collections;
using System.Collections.Generic;
using Modifiers;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : LivingEntity
{
    public int inventorySize = 5;
    public ModifierInventory Inventory;
    
    void Start()
    {
        Heal(MaxHealth);
        Inventory = new ModifierInventory(inventorySize);

        Inventory.Equip(new Ricochet());
    }

    void Update()
    {
        Inventory.Update();
    }
}
