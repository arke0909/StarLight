﻿using System;
using Code.Core.StatSystem;
using UnityEngine;

namespace Code.Entities
{
    public class EntityHealth : MonoBehaviour, IEntityComponent, IAfterInit
    {
        [SerializeField] private StatSo hpStat;
        public float maxHealth;
        private float _currentHealth;
        public float CurrentHealth => _currentHealth;
        
        private Entity _entity;
        private EntityStat _statCompo;
        private EntityFeedbackData _feedbackData;

        #region Initialize section

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _statCompo = _entity.GetCompo<EntityStat>();
            _feedbackData = _entity.GetCompo<EntityFeedbackData>();
        }
        
        public void AfterInit()
        {
            _statCompo.GetStat(hpStat).OnValueChange += HandleHPChange;
            _currentHealth = maxHealth = _statCompo.GetStat(hpStat).Value;
            _entity.OnDamage += ApplyDamage;
        }

        private void OnDestroy()
        {
            _statCompo.GetStat(hpStat).OnValueChange -= HandleHPChange;
            _entity.OnDamage -= ApplyDamage;
        }

        #endregion
        

        private void HandleHPChange(StatSo stat, float current, float previous)
        {
            maxHealth = current;
            _currentHealth = Mathf.Clamp(_currentHealth + current - previous, 1f, maxHealth);
            //체력변경으로 인해 사망하는 일은 없도록
        }

        public void ApplyDamage(float damage)
        {
            if (_entity.IsDead) return; //이미 죽은 녀석입니다.
            
            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, maxHealth);
            AfterHitFeedbacks();
        }

        private void AfterHitFeedbacks()
        {
            _entity.OnHit?.Invoke();

            if (_currentHealth <= 0)
            {
                _entity.OnDead?.Invoke();
            }
        }
    }
}