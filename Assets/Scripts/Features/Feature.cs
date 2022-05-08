using System;

public abstract class Feature
{
    private int _value;
    public Action<int> Changed;
    protected abstract void Awake();

    public int Value
    {
        get => _value;
        private set
        {
            _value = value;
            Changed?.Invoke(_value);
        }
    }

    public void IncreaseValue(int value)
    {
        Value += value;
    }
    
    public void DecreaseValue(int value)
    {
        Value -= value;
    }
}