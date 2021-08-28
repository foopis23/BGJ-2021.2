using CallbackEvents;
using Modifiers;
using UnityEngine;

public class Player : LivingEntity
{
    public CPMPlayer playerMovement;
    public int inventorySize = 5;
    public ModifierInventory Inventory;
    public float statusEffectTickSpeed = 20.0f;
    private float _lastStatusEffectTick;

    private float _baseMoveSpeed;
    private float _baseStrafeSpeed;
    private float _baseMoveAcc;

    private void Start()
    {
        InitEvent();
        
        playerMovement ??= GetComponent<CPMPlayer>();
        _baseMoveSpeed = playerMovement.moveSpeed;
        _baseStrafeSpeed = playerMovement.sideStrafeSpeed;
        _baseMoveAcc = playerMovement.runAcceleration;
        
        _lastStatusEffectTick = -statusEffectTickSpeed;
        
        Heal(MaxHealth);
        Inventory = new ModifierInventory(inventorySize);
    }

    private void Update()
    {
        if (Time.time - _lastStatusEffectTick >= statusEffectTickSpeed)
        {
            var moveSpeedContext = EventSystem.Current.FireFilter<PlayerStatusEffectContext>(
                new PlayerStatusEffectContext(_baseMoveSpeed, _baseStrafeSpeed, _baseMoveAcc, this));

            playerMovement.moveSpeed = moveSpeedContext.MoveSpeed;
            playerMovement.sideStrafeSpeed = moveSpeedContext.SideStrafeSpeed;
            playerMovement.runAcceleration = moveSpeedContext.MoveAcceleration;
            
            _lastStatusEffectTick = Time.time;
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

public class PlayerStatusEffectContext : EventContext
{
    public readonly Player Player;
    
    public readonly float BaseMoveSpeed;
    public float MoveSpeed;

    public readonly float BaseSideStrafeSpeed;
    public float SideStrafeSpeed;

    public readonly float BaseMoveAcceleration;
    public float MoveAcceleration;

    public PlayerStatusEffectContext(float baseMoveSpeed, float baseSideStrafeSpeed, float baseMoveAcceleration, Player player)
    {
        Player = player;
        BaseMoveSpeed = baseMoveSpeed;
        BaseSideStrafeSpeed = baseSideStrafeSpeed;
        BaseMoveAcceleration = baseMoveAcceleration;

        MoveSpeed = baseMoveSpeed;
        SideStrafeSpeed = BaseSideStrafeSpeed;
        MoveAcceleration = BaseMoveAcceleration;
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