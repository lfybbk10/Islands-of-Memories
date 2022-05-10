public class RuntimeContext : Singleton<RuntimeContext>
{
    #region Features

    public Strength strength;
    public Intelligence intelligence;
    public Stamina stamina;
 
    #endregion


    #region Hero

    public Hero hero;
    public Health health;
    public Combat combat;
    public Dropper dropper;
    public Equiper equiper;

    #endregion


    #region Inventory

    public UIInventoryFastSlot pickedSlot;
    public Inventory inventory;

    #endregion


}