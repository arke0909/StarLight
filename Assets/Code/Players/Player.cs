using System;
using System.Collections;
using Code.Entities;
using Code.Entities.FSM;
using UnityEngine;

namespace Code.Players
{
    public class Player : Entity
    {
        [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }
        [SerializeField] private StateListSO stateList;
        [SerializeField] private float dodgeSpeed = 7f;
        [SerializeField] private float moveSpeed = 4f;
        [SerializeField] private float dodgeTime = 0.7f;

        private StateMachine _stateMachine;
        private int _dodgeLayer;
        private int _originLayer;
        private float _originSpeed;
        
        [HideInInspector] public bool isDodging = false;

        protected override void Awake()
        {
            base.Awake();
            _stateMachine = new StateMachine(this, stateList);
            _originSpeed = moveSpeed;
            _dodgeLayer = LayerMask.NameToLayer("Dodge");
            _originLayer = gameObject.layer;
        }

        private void Start()
        {
            StateChange("IDLE");
        }

        private void Update()
        {
            _stateMachine.UpdateStateMachine();
        }

        protected override void HandleHit()
        {
        }

        protected override void HandleDead()
        {
        }

        private IEnumerator DodgeCoroutine()
        {
            gameObject.layer = _dodgeLayer;
            moveSpeed = dodgeSpeed;
            yield return new WaitForSeconds(dodgeTime);
            gameObject.layer = _originLayer;
            moveSpeed = _originSpeed;
        }

        public void StateChange(string newState)
           => _stateMachine.ChangeState(newState);
    }
}