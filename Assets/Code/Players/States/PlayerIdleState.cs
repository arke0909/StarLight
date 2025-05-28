using Code.Animators;
using Code.Entities;
using UnityEngine;

namespace Code.Players.States
{
    public class PlayerIdleState : PlayerState
    {
        public PlayerIdleState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
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
            if(_player.PlayerInput.InputVector.magnitude > 0.1f)
                _player.StateChange("MOVE");
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