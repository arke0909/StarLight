using Code.Animators;
using Code.Entities;
using UnityEngine;

namespace Code.Players.States
{
    public class PlayerMoveState : PlayerState
    {
        public PlayerMoveState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _moveCompo.StopImmediately();
            _player.PlayerInput.OnSpaceEvent += HandleDodgeEvent;
        }

        public override void Update()
        {
            Vector2 moveDir = _player.PlayerInput.InputVector;
            if (moveDir.magnitude < 0.1f)
                _player.StateChange("IDLE");
            
            _moveCompo.SetMoveDirection(moveDir);
        }

        public override void Exit()
        {
            _player.PlayerInput.OnSpaceEvent -= HandleDodgeEvent;
            base.Exit();
        }

        private void HandleDodgeEvent()
        {
            if (!_player.isDodging)
                _player.StateChange("DODGE");
        }
    }
}