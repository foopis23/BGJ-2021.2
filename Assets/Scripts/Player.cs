using CallbackEvents;
using Modifiers;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : LivingEntity
{
    public CPMPlayer playerMovement;
    public int inventorySize = 5;
    public ModifierInventory Inventory;
    [FormerlySerializedAs("statusEffectTickSpeed")] public float passiveModifierTickSpeed = 20.0f;
    private float _lastPassiveModifierTick;

    private void Start()
    {
        _lastPassiveModifierTick = 0;
        Init();
        Inventory = new ModifierInventory(inventorySize);
    }

    private void Update()
    {
        if (Time.time - _lastPassiveModifierTick >= passiveModifierTickSpeed)
        {
            EventSystem.Current.FireEvent(new OnPlayerPassiveModifierTick(this));
        }
        Inventory.Update();
    }

    public override void Damage(float amount)
    {
        base.Damage(amount);
        EventSystem.Current.FireEvent(new OnPlayerDamageContext {Player = this, DamageAmount = amount});
    }

    protected override void OnDeath()
    {
        EventSystem.Current.FireEvent(new OnPlayerDeathContext(){Player = this});
    }
}

public class OnPlayerPassiveModifierTick : EventContext
{
    public readonly Player Player;

    public OnPlayerPassiveModifierTick(Player player)
    {
        Player = player;
    }
}

public class OnPlayerDamageContext : EventContext
{
    public Player Player;
    public float DamageAmount;
}

public class OnPlayerDeathContext : EventContext
{
    public Player Player;
}