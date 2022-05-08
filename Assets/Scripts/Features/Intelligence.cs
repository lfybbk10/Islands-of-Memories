public class Intelligence : Feature
{
    protected override void Awake()
    {
        RuntimeContext.Instance.intelligence = this;
    }
}