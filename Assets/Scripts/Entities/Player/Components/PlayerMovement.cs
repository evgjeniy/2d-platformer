using UnityEngine;
using Utils;

namespace Entities.Player.Components
{
    [System.Serializable]
    public class PlayerMovement
    {
        [SerializeField, Range(0.0f, 100.0f)] private float moveSpeed = 25.0f;
        [SerializeField, Range(0.0f, 0.3f)] private float movementSmoothing = 0.05f;
        [SerializeField] private bool isAirControl = true;

        private PlayerEntity _player;

        private Vector2 _velocity;

        public void RegisterContext(PlayerEntity player) => _player = player;

        public void Move(float move)
        {
            if (_player == null) return;
            if (!_player.Controller.IsGrounded && !isAirControl) return;
            
            var velocity = _player.Rigidbody.velocity;
            var targetVelocity = new Vector2(move * moveSpeed * 10.0f, velocity.y);
            
            _player.Rigidbody.velocity = Vector2.SmoothDamp(velocity, targetVelocity, ref _velocity, movementSmoothing);
            
            if (move != 0.0f) _player.LookAt(move);
        }
    }
}