using Assets.HeroEditor.Common.CharacterScripts;
using InputScripts;
using Overlaps2D;
using UnityEngine;
using UnityEngine.Events;

namespace Entities.Player
{
    [System.Serializable]
    public class PlayerController : EntityController
    {
        [SerializeField] private float jumpForce = 250f;
        [SerializeField, Range(0.0f, 100.0f)] private float moveSpeed = 25.0f;
        [SerializeField, Range(0.0f, 0.3f)] private float movementSmoothing = 0.05f;
        [SerializeField] private bool isAirControl = true;
        [SerializeField] private CircleOverlapNonAlloc2D groundCheckOverlap;

        private PlayerEntity _playerEntity;
        
        private Vector2 _velocity = Vector2.zero;
        private float _moveDirection;
        private bool _isGrounded;
        private bool _isJump;
        
        public Rigidbody2D Rigidbody { get; private set; }
        private IInputBehaviour _input;

        public void OnDrawGizmosSelected() => groundCheckOverlap.TryDrawGizmos();

        protected override void Awake<T1, T2>(Entity<T1, T2> entity)
        {
            if ((_playerEntity = entity as PlayerEntity) == null) return;

            Rigidbody = _playerEntity.GetComponent<Rigidbody2D>();
            _input = _playerEntity.GetComponent<IInputBehaviour>();
        }

        protected override void Update()
        {
            _moveDirection = _input.GetMoveDirection();
            _isJump = _input.GetJumpState();
        }

        protected override void FixedUpdate()
        {
            CheckGrounded();
            Move(_moveDirection * Time.fixedDeltaTime, _isJump);
        }

        private void CheckGrounded()
        {
            _isGrounded = false;

            groundCheckOverlap.Perform();
            for (var i = 0; i < groundCheckOverlap.Size; i++)
            {
                if (groundCheckOverlap.Colliders[i].gameObject == _playerEntity.gameObject) continue;
                _isGrounded = true;
                break;
            }
        }

        private void Move(float move, bool jump)
        {
            if (_isGrounded || isAirControl)
            {
                var velocity = Rigidbody.velocity;
                var targetVelocity = new Vector2(move * moveSpeed * 10.0f, velocity.y);
				
                Rigidbody.velocity = Vector2.SmoothDamp(velocity, targetVelocity, ref _velocity, movementSmoothing);
				
                if (move != 0.0f) Flip(move);
            }

            if (_isGrounded && jump)
            {
                _isGrounded = false;
                Rigidbody.AddForce(new Vector2(0f, jumpForce));
            }
        }

        private void Flip(float direction)
        {
            var scale = _playerEntity.transform.localScale.z;
            _playerEntity.transform.localScale = new Vector3(Mathf.Sign(direction), 1.0f, 1.0f) * scale;
        }
    }

    [System.Serializable] 
    public class CharacterStateEvent : UnityEvent<CharacterState> {}
}