using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputScripts
{
    public class PlayerInput
    {
        public PlayerInputType PlayerInputType { get; set; }

        private readonly InputActions _input;
        
        public Vector2 MoveDirection => PlayerInputType switch
        {
            PlayerInputType.FirstPlayer => _input.FirstPlayer.Move.ReadValue<Vector2>(),
            PlayerInputType.SecondPlayer => _input.SecondPlayer.Move.ReadValue<Vector2>(),
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