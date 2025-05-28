using System;
using UnityEngine;

namespace Code.Core.StatSystem
{
    [Serializable]
    public class StatOverride
    {
        [SerializeField] private StatSo stat;
        [SerializeField] private bool isUseOverride;
        [SerializeField] private float overrideValue;
        
        public StatOverride(StatSo stat) => this.stat = stat;

        public StatSo CreateStat()
        {
            StatSo newStat = stat.Clone() as StatSo;
            Debug.Assert(newStat != null, $"{stat.statName} clone failed");
            
            if (isUseOverride)
                newStat.BaseValue = overrideValue;
            
            return newStat;
        }
    }
}