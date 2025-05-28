﻿using System;
using Code.Core.StatSystem;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Entities
{
    public class EntityMover : MonoBehaviour, IEntityComponent, IAfterInit
    {
        [SerializeField] private StatSo moveSpeedStat;

        #region Member field

        private Vector2 _moveVector;
        private float _moveSpeed = 6f;
        private float _moveSpeedMultiplier;
        private Rigidbody _rbCompo;
        private EntityStat _statCompo;

        #endregion

        public bool CanManualMove { get; set; } = true; //넉백당하거나 기절시 이동불가

        #region Init section

        public void Initialize(Entity entity)
        {
            _rbCompo = entity.GetComponent<Rigidbody>();
            _statCompo = entity.GetCompo<EntityStat>();
            _moveSpeedMultiplier = 1f;
        }

        public void AfterInit()
        {
            _statCompo.GetStat(moveSpeedStat).OnValueChange += HandleMoveSpeedChange;

            _moveSpeed = _statCompo.GetStat(moveSpeedStat).Value;
        }

        private void OnDestroy()
        {
            _statCompo.GetStat(moveSpeedStat).OnValueChange -= HandleMoveSpeedChange;
        }

        #endregion

        public void SetMoveSpeedMultiplier(float value)
            => _moveSpeedMultiplier = value;

        public void SetMoveDirection(Vector2 value)
        {
            _moveVector = value;
        }
        
        private void HandleMoveSpeedChange(StatSo stat, float current, float previous)
            => _moveSpeed = current;
        
        public void AddForceToEntity(Vector2 force)
            => _rbCompo.AddForce(force, ForceMode.Impulse);
        
        private void FixedUpdate()
        {
            if (CanManualMove)
                _rbCompo.linearVelocity = _moveVector * _moveSpeed * _moveSpeedMultiplier;
        }

        public void StopImmediately()
        {
            _rbCompo.linearVelocity = Vector3.zero;
        }
    }
}