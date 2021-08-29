using CallbackEvents;
using Modifiers;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : LivingEntity
{
    public CPMPlayer playerMovement;
    public int inventorySize = 5;
    public CardInventory Inventory;
    [FormerlySerializedAs("statusEffectTickSpeed")] public float passiveModifierTickSpeed = 20.0f;
    private float _lastPassiveModifierTick;

    public CardObject[] startCards;
    
    public CardDeck cardDeck;
    public int purchaseCardPoints;

    void Awake()
    {
        Inventory = new CardInventory(inventorySize);
    }

    private void Start()
    {
        _lastPassiveModifierTick = -passiveModifierTickSpeed;
        Init();
        baseWalkSpeed = playerMovement.moveSpeed;
        baseStrafeSpeed = playerMovement.sideStrafeSpeed;
        baseMoveAcceleration = playerMovement.runAcceleration;

        foreach (var obj in startCards)
        {
            var card = (CardObject) Instantiate(obj);
            card.Init();
            Inventory.Equip(card);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Buy Card"))
        {
            cardDeck.PurchaseCard(this);
        }
        
        playerMovement.moveSpeed = WalkSpeed;
        playerMovement.sideStrafeSpeed = StrafeSpeed;
        playerMovement.runAcceleration = MoveAcceleration;

        if (Time.time - _lastPassiveModifierTick >= passiveModifierTickSpeed)
        {
            EventSystem.Current.FireEvent(new OnPlayerPassiveModifierTick(this, Time.time - _lastPassiveModifierTick));
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
    public readonly float TickTime;

    public OnPlayerPassiveModifierTick(Player player, float tickTime)
    {
        Player = player;
        TickTime = tickTime;
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