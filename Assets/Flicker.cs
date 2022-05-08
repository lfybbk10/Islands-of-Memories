using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _meshRenderer;
    [SerializeField] private Color _color;
    private Color _startColor;
    private static readonly int FresnelColor = Shader.PropertyToID("_FresnelColor");
    private readonly List<Material> _materials = new List<Material>();

    private void Awake()
    {
        _startColor = _meshRenderer.material.GetColor(FresnelColor);
        foreach (var material in _meshRenderer.materials)
            _materials.Add(material);
    }

    public void Create()
    {
        foreach (var material in _materials)
        {
            material.DOColor(_color, FresnelColor, 0.3f).OnComplete(() =>
            {
                material.DOColor(_startColor, FresnelColor, 0.3f);
            });
        }
    }
}