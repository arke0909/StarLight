using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Players
{
    [CreateAssetMenu(fileName = "PlayerInputSO", menuName = "SO/PlayerInputSO")]
    public class PlayerInputSO : ScriptableObject, Input.IPlayerActions
    {
        public event Action OnSpaceEvent;
    
        public Vector2 InputVector { get; private set; }
        public Vector2 MouseVector { get; private set; }

        private Input input;
        
        private void OnEnable()
        {
            if (input == null)
            {
                input = new Input();
                input.Player.SetCallbacks(this);
            }
            
            input.Player.Enable();
        }

        private void OnDisable()
        {
            input.Disable();
        }


        public void OnMove(InputAction.CallbackContext context)
        {
            InputVector = context.ReadValue<Vector2>();
        }

        public void OnSpace(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnSpaceEvent?.Invoke();
        }

        public void OnMouse(InputAction.CallbackContext context)
        {
            MouseVector = context.ReadValue<Vector2>();
        }
    }
}