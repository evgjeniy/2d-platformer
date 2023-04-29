using System;
using UnityEngine.InputSystem;

namespace InputScripts
{
    public class PlayerInput
    {
        public PlayerInputType PlayerInputType { get; set; }

        private readonly InputActions _input;
        
        public float MoveDirection => PlayerInputType switch
        {
            PlayerInputType.FirstPlayer => _input.FirstPlayer.Move.ReadValue<float>(),
            PlayerInputType.SecondPlayer => _input.SecondPlayer.Move.ReadValue<float>(),
            _ => throw new ArgumentOutOfRangeException(nameof(PlayerInputType))
        };

        public InputAction JumpAction => PlayerInputType switch
        {
            PlayerInputType.FirstPlayer => _input.FirstPlayer.Jump,
            PlayerInputType.SecondPlayer => _input.SecondPlayer.Jump,
            _ => throw new ArgumentOutOfRangeException(nameof(PlayerInputType))
        };

        public InputAction AttackAction => PlayerInputType switch
        {
            PlayerInputType.FirstPlayer => _input.FirstPlayer.Attack,
            PlayerInputType.SecondPlayer => _input.SecondPlayer.Attack,
            _ => throw new ArgumentOutOfRangeException(nameof(PlayerInputType))
        };

        public PlayerInput() => _input = new InputActions();

        public void Enable() => _input?.Enable();
        
        public void Disable() => _input?.Disable();
    }
}