using System;
using Modifiers;
using UnityEngine;

public class ModifierInventory
{
    private readonly IModifier[] _modifiers;
    public float TickSpeed;
    private float _lastTick;
    
    public ModifierInventory(int inventorySize, float tickSpeed = 1.0f)
    {
        _modifiers = new IModifier[inventorySize];
        TickSpeed = tickSpeed;
        _lastTick = 0;
    }

    /**
     *  Equip modifier in inventory slot. If slot is equal to -1, function will try to fill the an empty slot.
     *  returns true if fills, returns false if inventory is full
     */
    public bool Equip(IModifier modifier, int slot = -1)
    {
        if (slot == -1) {
            for (var i = 0; i < _modifiers.Length; i++)
            {
                if (_modifiers[i] != null) continue;
                
                SwapSlot(modifier, i);
                return true;
            }

            return false;
        }

        if (slot < 0 || slot > _modifiers.Length) throw new Exception("Inventory Slot Out Of Bounds");

        SwapSlot(modifier, slot);
        return true;
    }

    public bool UnEquip(int slot)
    {
        if (slot < 0 || slot > _modifiers.Length) throw new Exception("Inventory Slot Out Of Bounds");

        if (_modifiers[slot] == null) return false;
        
        _modifiers[slot].Deactivate();
        _modifiers[slot] = null;
        return true;
    }

    private void SwapSlot(IModifier modifier, int slot)
    {
        ClearSlot(slot);
        _modifiers[slot] = modifier;
        _modifiers[slot].Activate();
    }

    private void ClearSlot(int slot)
    {
        if (_modifiers[slot] != null)
        {
            _modifiers[slot].Deactivate();
        }

        _modifiers[slot] = null;
    }

    public void Update()
    {
        if (!(Time.time - _lastTick > TickSpeed)) return;
        
        foreach (var modifier in _modifiers)
        {
            modifier?.Update();
        }

        _lastTick = Time.time;
    }
}
