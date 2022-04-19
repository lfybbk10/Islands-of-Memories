using System.Collections.Generic;
using UnityEngine.Rendering;



public class Stat
{
    public float Value { get; private set; }
    public  string Name { get; private set; }

    public Stat(float _value, string _name)
    {
        Value = _value;
        Name = _name;
    }

    public void IncStat(float additional)
    {
        Value += additional;
    }
}

public static class HeroStats
{
    public static List<Stat> _stats = new List<Stat>()
    {
        new Stat(0f, "Strength"),
        new Stat(0f, "Agility"),
        new Stat(0f, "Intelligence"),
        new Stat(0f, "Stamina"),
    };

}