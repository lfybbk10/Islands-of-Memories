public class Stamina : Feature
{
    protected override void Awake()
    {
        RuntimeContext.Instance.stamina = this;
    }
}