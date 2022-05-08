public class RuntimeContext : Singleton<RuntimeContext>
{
    #region Features

    public Strength strength;
    public Intelligence intelligence;
    public Stamina stamina;

    #endregion


    #region Hero

    public Hero hero;
    public Combat combat;
    public Equiper equiper;

    #endregion
}