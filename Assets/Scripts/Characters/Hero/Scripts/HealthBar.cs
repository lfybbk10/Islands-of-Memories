using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _healthIndicator;
    [SerializeField] private Slider _damageIndicator;
    private float _health;

    public float Value { get; set; }

    public void SetHealth(float heath)
    {
        _health = heath;
        Value = _health;
        StartCoroutine(nameof(SetHealthIndicator));
    }
    
    private IEnumerator SetHealthIndicator()
    {
        while (Value >= 0)
        {
            _healthIndicator.value = Value / _health;
            var randomTime = Random.Range(0.8f, 1.2f);
            _damageIndicator.DOValue(Value / _health, 0.1f).SetDelay(randomTime - 0.25f);
            yield return new WaitForSeconds(randomTime);
        }
    }
}