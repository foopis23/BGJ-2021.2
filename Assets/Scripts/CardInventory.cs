using System;
using Modifiers;
using UnityEngine;

public class CardInventory
{
    public CardObject[] _cards;
    public float TickSpeed;
    private float _lastTick;
    
    public CardInventory(int inventorySize, float tickSpeed = 1.0f)
    {
        _cards = new CardObject[inventorySize];
        TickSpeed = tickSpeed;
        _lastTick = 0;
    }

    /**
     *  Equip modifier in inventory slot. If slot is equal to -1, function will try to fill the an empty slot.
     *  returns true if fills, returns false if inventory is full
     */
    public bool Equip(CardObject card, int slot = -1)
    {
        if (slot == -1) {
            for (var i = 0; i < _cards.Length; i++)
            {
                if (_cards[i] != null) continue;
                
                SwapSlot(card, i);
                return true;
            }

            return false;
        }

        if (slot < 0 || slot > _cards.Length) throw new Exception("Inventory Slot Out Of Bounds");

        SwapSlot(card, slot);
        return true;
    }

    public bool UnEquip(int slot)
    {
        if (slot < 0 || slot > _cards.Length) throw new Exception("Inventory Slot Out Of Bounds");

        if (_cards[slot] == null) return false;
        
        foreach(var modifier in _cards[slot].Modifiers)
        {
            modifier.Deactivate();
        }

        _cards[slot] = null;
        return true;
    }

    private void SwapSlot(CardObject card, int slot)
    {
        ClearSlot(slot);
        _cards[slot] = card;
        foreach(var modifier in _cards[slot].Modifiers)
        {
            modifier.Activate();
        }
    }

    private void ClearSlot(int slot)
    {
        if (_cards[slot] != null)
        {
            foreach(var modifier in _cards[slot].Modifiers)
            {
                modifier.Deactivate();
            }
        }

        _cards[slot] = null;
    }

    public void Update()
    {
        if (!(Time.time - _lastTick > TickSpeed)) return;
        
        foreach (var card in _cards)
        {
            if(card != null)
            {
                foreach(var modifier in card.Modifiers)
                {
                    modifier?.Update();
                }
            }
        }

        _lastTick = Time.time;
    }
}
