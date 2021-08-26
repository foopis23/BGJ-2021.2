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

    private void Start()
    {
        playerMovement ??= GetComponent<CPMPlayer>();
        _baseMoveSpeed = playerMovement.moveSpeed;
        _lastStatusEffectTick = 0;
        
        Heal(MaxHealth);
        Inventory = new ModifierInventory(inventorySize);
        Inventory.Equip(new GrapeShot());
        Inventory.Equip(new LegDay());
        Inventory.Equip(new LegDay());
        Inventory.Equip(new LegDay());
        Inventory.Equip(new LegDay());
    }

    private void Update()
    {
        if (Time.time - _lastStatusEffectTick >= statusEffectTickSpeed)
        {
            var moveSpeedContext = EventSystem.Current.FireFilter<PlayerMoveSpeedFilterContext>(
                new PlayerMoveSpeedFilterContext(_baseMoveSpeed, this));

            playerMovement.moveSpeed = moveSpeedContext.MoveSpeed;
            _lastStatusEffectTick = Time.time;
        }
        
        Inventory.Update();
    }
}

public class PlayerMoveSpeedFilterContext : EventContext
{
    public readonly Player Player;
    public readonly float BaseMoveSpeed;
    public float MoveSpeed;

    public PlayerMoveSpeedFilterContext(float baseMoveSpeed, Player player)
    {
        BaseMoveSpeed = baseMoveSpeed;
        Player = player;
        MoveSpeed = baseMoveSpeed;
    }
}