using System.Linq;
using Overlaps2D;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities.Player
{
    [System.Serializable]
    public class PlayerJump
    {
        [SerializeField] private float jumpForce = 250f;
        [SerializeField] private CircleOverlap2D groundCheckOverlap;

        private PlayerEntity _player;

        public void RegisterContext(PlayerEntity context) => _player = context;

        public bool CheckGrounded()
        {
            groundCheckOverlap.Perform();
            return groundCheckOverlap.Colliders.Any(collider => collider.gameObject != _player.gameObject);
        }
        
        public void Jump(InputAction.CallbackContext callbackContext)
        {
            if (!_player.Controller.IsGrounded) return;
            
            _player.Controller.IsGrounded = false;
            _player.Rigidbody.AddForce(new Vector2(0f, jumpForce));
        }

        public void TryDrawGizmos() => groundCheckOverlap.TryDrawGizmos();
    }
}