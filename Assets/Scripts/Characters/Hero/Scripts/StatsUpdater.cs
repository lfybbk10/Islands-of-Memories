using System;
using TMPro;
using UnityEngine;
using System.Linq;
using Random = System.Random;


[RequireComponent(typeof(TMP_Text))]

    public class StatsUpdater : MonoBehaviour
    {
        private TMP_Text _text;
        private const string DISPLAY = "{0} : {1} \n";

        private void Start()
        {
            _text = GetComponent<TMP_Text>();

        }

        private void Update()
        {
            _text.text = String.Empty;

            Random random = new Random(); 
            HeroStats._stats.ForEach(x =>  x.IncStat(random.Next(10, 100)));
            
            foreach (Stat stat in  HeroStats._stats)
            {
                _text.text += String.Format(DISPLAY, stat.Name, stat.Value);
            }
            
        }
    }
