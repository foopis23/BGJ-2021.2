using Modifiers;

public class Player : LivingEntity
{
    public int inventorySize = 5;
    public ModifierInventory Inventory;
    
    void Start()
    {
        Heal(MaxHealth);
        Inventory = new ModifierInventory(inventorySize);
        Inventory.Equip(new GrapeShot());
    }

    void Update()
    {
        Inventory.Update();
    }
}
