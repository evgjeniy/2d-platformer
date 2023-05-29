using System.Linq;
using Overlaps2D;
using UnityEngine;

namespace Entities.Player.PlayerComponents
{
    [System.Serializable]
    public class PlayerJump
    {
        public enum State { Air, Grounded, Ladder }
     
        [SerializeField] private CircleOverlap2D groundCheckOverlap;
        [SerializeField, Min(0.0f)] private float jumpVelocityY = 10.0f;
        [SerializeField, Min(1.0f)] private float ladderSpeed = 3.0f;
        [SerializeField] private LayerMask ladderLayer;

        private PlayerEntity _player;
        private float? _tempGravityScale;

        public State JumpState { get; private set; } 

        public void RegisterContext(PlayerEntity context) => _player = context;

        public void CheckJumpState()
        {
            groundCheckOverlap.Perform();

            var colliders = groundCheckOverlap.Colliders;
            
            if (colliders.Any(collider => Mathf.Abs(Mathf.Pow(2, collider.gameObject.layer) - ladderLayer.value) < 0.01f))
            {
                JumpState = State.Ladder;
                _tempGravityScale ??= _player.Rigidbody.gravityScale;
                _player.Rigidbody.gravityScale = 0.0f;
                _player.Rigidbody.velocity = new Vector2(_player.Rigidbody.velocity.x, 0.0f);
            }
            else
            {
                if (_tempGravityScale != null) _player.Rigidbody.gravityScale = _tempGravityScale.Value;
                JumpState = colliders.Any(collider => collider.gameObject != _player.gameObject) ? State.Grounded : State.Air;
            }
        }
        
        public void TryJump(float jumpDirection)
        {
            if (jumpDirection == 0.0f) return;
            
            switch (JumpState)
            {
                case State.Ladder: Ladder(jumpDirection); break;
                case State.Grounded: if (jumpDirection > 0.0f) Jump(); break;
                case State.Air: break;
                default: throw new System.ArgumentOutOfRangeException(nameof(JumpState));
            }
        }

        private void Jump() => _player.Rigidbody.velocity = new Vector2(_player.Rigidbody.velocity.x, jumpVelocityY);

        private void Ladder(float moveDirectionY) => _player.Rigidbody.MovePosition(
            _player.position + new Vector3(0.0f, moveDirectionY * ladderSpeed));

        public void TryDrawGizmos() => groundCheckOverlap.TryDrawGizmos();
    }
}