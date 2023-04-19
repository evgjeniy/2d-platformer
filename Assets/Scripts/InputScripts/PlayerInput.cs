using System;
using UnityEngine.InputSystem;

namespace InputScripts
{
    public class PlayerInput
    {
        public PlayerInputType PlayerInputType { get; set; }

        private readonly InputActions _input;

        public PlayerInput() => _input = new InputActions();

        public void Enable() => _input?.Enable();
        
        public void Disable() => _input?.Disable();
        
        public float GetMoveDirection() => PlayerInputType switch
        {
            PlayerInputType.FirstPlayer => _input.FirstPlayer.Move.ReadValue<float>(),
            PlayerInputType.SecondPlayer => _input.SecondPlayer.Move.ReadValue<float>(),
            _ => throw new ArgumentOutOfRangeException(nameof(PlayerInputType))
        };

        public void RegisterJumpAction(Action<InputAction.CallbackContext> onJump)
        {
            switch (PlayerInputType)
            {
                case PlayerInputType.FirstPlayer: _input.FirstPlayer.Jump.performed += onJump; break;
                case PlayerInputType.SecondPlayer: _input.SecondPlayer.Jump.performed += onJump; break;
                default: throw new ArgumentOutOfRangeException(nameof(PlayerInputType));
            }
        }

        public void RegisterAttackAction(Action<InputAction.CallbackContext> onAttack)
        {
            switch (PlayerInputType)
            {
                case PlayerInputType.FirstPlayer: _input.FirstPlayer.Attack.performed += onAttack; break;
                case PlayerInputType.SecondPlayer: _input.SecondPlayer.Attack.performed += onAttack; break;
                default: throw new ArgumentOutOfRangeException(nameof(PlayerInputType));
            }
        }
    }
}