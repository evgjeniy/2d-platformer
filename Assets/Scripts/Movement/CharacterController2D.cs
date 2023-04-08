using Assets.HeroEditor.Common.CharacterScripts;
using InputScripts;
using Overlap.Overlaps2D;
using UnityEngine;
using UnityEngine.Events;

namespace Movement
{
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(IInputBehaviour))]
	public class CharacterController2D : MonoCashed<IInputBehaviour, Rigidbody2D>
	{
		[SerializeField] private float jumpForce = 200f;
		[SerializeField, Range(0.0f, 100.0f)] private float moveSpeed = 30.0f;
		[SerializeField, Range(0.0f, 0.3f)] private float movementSmoothing = 0.05f;
		[SerializeField] private bool airControl;
		[SerializeField] private CircleOverlapNonAlloc2D groundCheckOverlap;

		private Vector2 _velocity = Vector2.zero;
		private bool _isFacingRight = true;
		private bool _grounded;

		private float _moveDirection;
		private bool _isJump;

		[Header("State Changes Listeners")]
		public CharacterStateEvent onStateChangedEvent;

		protected override void PostAwake() => onStateChangedEvent ??= new CharacterStateEvent();

		private void Update()
		{
			_moveDirection = First.GetMoveDirection();
			_isJump = First.GetJumpState();
		}

		private void FixedUpdate()
		{
			_grounded = false;

			groundCheckOverlap.Perform();
			for (var i = 0; i < groundCheckOverlap.Size; i++)
			{
				if (groundCheckOverlap.Colliders[i].gameObject == gameObject) continue;
				_grounded = true;
				break;
			}
			
			Move(_moveDirection * Time.fixedDeltaTime, _isJump);
		}

		private void Move(float move, bool jump)
		{
			if (_grounded || airControl)
			{
				var velocity = Second.velocity;
				var targetVelocity = new Vector2(move * moveSpeed * 10.0f, velocity.y);
				
				Second.velocity = Vector2.SmoothDamp(velocity, targetVelocity, ref _velocity, movementSmoothing);

				if ((move > 0 && !_isFacingRight) || (move < 0 && _isFacingRight))
				{
					Flip();
					onStateChangedEvent?.Invoke(CharacterState.Run);
				}
				else onStateChangedEvent?.Invoke(CharacterState.Idle);
			}

			if (_grounded && jump)
			{
				_grounded = false;
				Second.AddForce(new Vector2(0f, jumpForce));
				onStateChangedEvent?.Invoke(CharacterState.Jump);
			}
		}

		private void Flip()
		{
			_isFacingRight = !_isFacingRight;

			var theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}

		private void OnDrawGizmosSelected() => groundCheckOverlap.TryDrawGizmos();
	}
	
	[System.Serializable] public class CharacterStateEvent : UnityEvent<CharacterState> {}
}