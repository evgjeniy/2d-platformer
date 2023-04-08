using System;
using UnityEngine;

namespace InputScripts
{
    public class InputBehaviour : MonoBehaviour, IInputBehaviour
    {
        [field: SerializeField] public PlayerInputType PlayerInputType { get; set; }

        private InputActions _input;

        public float GetMoveDirection() => PlayerInputType switch
        {
            PlayerInputType.FirstPlayer => _input.FirstPlayer.Move.ReadValue<float>(),
            PlayerInputType.SecondPlayer => _input.SecondPlayer.Move.ReadValue<float>(),
            _ => throw new ArgumentOutOfRangeException()
        };

        public bool GetJumpState() => PlayerInputType switch
        {
            PlayerInputType.FirstPlayer => _input.FirstPlayer.Jump.IsPressed(),
            PlayerInputType.SecondPlayer => _input.SecondPlayer.Jump.IsPressed(),
            _ => throw new ArgumentOutOfRangeException(nameof(PlayerInputType))
        };

        private void Awake() => _input = new InputActions();
        private void OnEnable() => _input.Enable();
        private void OnDisable() => _input.Disable();
    }
}