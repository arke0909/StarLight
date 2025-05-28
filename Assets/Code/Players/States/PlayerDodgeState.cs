using Code.Animators;
using Code.Entities;

namespace Code.Players.States
{
    public class PlayerDodgeState : PlayerState
    {
        public PlayerDodgeState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _player.isDodging = true;
            
            _moveCompo.AddForceToEntity(_player.PlayerInput.InputVector);
        }

        public override void Update()
        {
            if (_isTriggerCall)
            {
                _player.StateChange("IDLE");
            }
        }

        public override void Exit()
        {
            _player.isDodging = false;
            base.Exit();
        }
    }
}