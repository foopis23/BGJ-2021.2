using System;
using Modifiers;
using UnityEngine;

public class CardInventory
{
    public const float Chaosity = 0.01f;

    public CardObject[] Cards;
    public float TickSpeed;
    private float _lastTick;
    
    public CardInventory(int inventorySize, float tickSpeed = 1.0f)
    {
        Cards = new CardObject[inventorySize];
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
            for (var i = 0; i < Cards.Length; i++)
            {
                if (Cards[i] != null) continue;
                
                SwapSlot(card, i);
                return true;
            }

            return false;
        }

        if (slot < 0 || slot > Cards.Length) throw new Exception("Inventory Slot Out Of Bounds");

        SwapSlot(card, slot);
        return true;
    }

    public bool UnEquip(int slot)
    {
        if (slot < 0 || slot > Cards.Length) throw new Exception("Inventory Slot Out Of Bounds");

        if (Cards[slot] == null) return false;
        
        foreach(var modifier in Cards[slot].Modifiers)
        {
            modifier.Deactivate();
        }

        Cards[slot] = null;
        return true;
    }

    private void SwapSlot(CardObject card, int slot)
    {
        ClearSlot(slot);
        Cards[slot] = card;
        foreach(var modifier in Cards[slot].Modifiers)
        {
            modifier.Activate();
        }
    }

    private void ClearSlot(int slot)
    {
        if (Cards[slot] != null)
        {
            foreach(var modifier in Cards[slot].Modifiers)
            {
                modifier.Deactivate();
            }
        }

        Cards[slot] = null;
    }

    public void Update()
    {
        if (!(Time.time - _lastTick > TickSpeed)) return;
        
        foreach (var card in Cards)
        {
            if(card != null)
            {
                foreach(var modifier in card.Modifiers)
                {
                    modifier?.Update();
                }

                if (UnityEngine.Random.Range(0.0f, 1.0f) < Chaosity)
                {
                    card.ChaosLevel = Mathf.Min(1.0f, card.ChaosLevel + 0.01f);
                }
            }
        }

        _lastTick = Time.time;
    }
}
