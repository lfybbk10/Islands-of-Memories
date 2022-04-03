using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Craft
{
    public class UICraftMenu : MonoBehaviour
    {
        [SerializeField] private Image _main;
        [SerializeField] private Text _text;
        [SerializeField] private Button _craft;
        private Action _crafted;

        private void Start()
        {
            _main.enabled = false;
            _craft.onClick.AddListener(() => { _crafted?.Invoke(); });
        }
        public void Init(Action crafted)
        {
            _crafted = crafted;
        }
        
        public void Replace(IRecipe recipe)
        {
            _text.text = recipe.Received.Info.Name;
            _main.transform.DOScale(new Vector3(0, 0, 0), 0.15f).OnComplete(() =>
            {
                _main.enabled = true;
                _main.sprite = recipe.Received.Info.Icon;
                _main.transform.DOScale(new Vector3(1, 1, 1), 0.15f);
            });
            //TODO дописать requiments.
        }
    }
}