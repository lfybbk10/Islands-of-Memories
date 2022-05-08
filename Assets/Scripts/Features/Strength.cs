public class Strength : Feature
{
    protected override void Awake()
    {
        RuntimeContext.Instance.strength = this;
    }
}