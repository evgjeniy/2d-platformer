using System;
using UnityEngine;

namespace CharacterMovement
{
    public class InputBehaviour : MonoBehaviour, IInputBehaviour
    {
        [field: SerializeField] public PlayerInputType PlayerInputType { get; set; }

        private InputActions _input;

        public Vector2 GetMoveDirection() => PlayerInputType switch
        {
            PlayerInputType.FirstPlayer => _input.FirstPlayer.Move.ReadValue<Vector2>(),
            PlayerInputType.SecondPlayer => _input.SecondPlayer.Move.ReadValue<Vector2>(),
            _ => throw new ArgumentOutOfRangeException()
        };

        private void Awake() => _input = new InputActions();
        private void OnEnable() => _input.Enable();
        private void OnDisable() => _input.Disable();
    }

    public enum PlayerInputType { FirstPlayer, SecondPlayer }
}