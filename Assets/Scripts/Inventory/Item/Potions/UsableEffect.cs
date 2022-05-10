using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "UsableEffect", menuName = "UsableEffect")]
public class UsableEffect : ScriptableObject, IUsableEffect
{
    [SerializeField] private int _strenght;
    [SerializeField] private int _stamina;
    [SerializeField] private int _health;
    public int Strength => _strenght;
    public int Health => _health;
    public int Stamina => _stamina;
}