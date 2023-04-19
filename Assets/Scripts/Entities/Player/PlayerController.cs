using Assets.HeroEditor.Common.CharacterScripts;
using InputScripts;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = InputScripts.PlayerInput;

namespace Entities.Player
{
    [System.Serializable]
    public class PlayerController : EntityController
    {
        [SerializeField] private PlayerInputType inputType;
        [SerializeField] private PlayerMovement movementComponent;
        [SerializeField] private PlayerJump jumpComponent;
        [SerializeField] private PlayerAttack attackComponent;

        private PlayerEntity _playerEntity;
        private PlayerInput _input;

        public bool IsGrounded { get; set; }

        protected override void Awake<T1, T2>(Entity<T1, T2> entity)
        {
            if ((_playerEntity = entity as PlayerEntity) == null) return;

            _input = new PlayerInput { PlayerInputType = inputType };
            _input.RegisterAttackAction(Attack);
            _input.RegisterJumpAction(jumpComponent.Jump);

            movementComponent.RegisterContext(_playerEntity);
            jumpComponent.RegisterContext(_playerEntity);
        }

        private void Attack(InputAction.CallbackContext callbackContext)
        {
            if (Random.Range(0, 2) == 1) 
                _playerEntity.Character.Jab();
            else 
                _playerEntity.Character.Slash();
        }

        protected override void FixedUpdate()
        {
            IsGrounded = jumpComponent.CheckGrounded();

            var moveDirection = _input.GetMoveDirection();

            _playerEntity.Character.SetState(IsGrounded
                ? moveDirection == 0.0f
                    ? CharacterState.Idle
                    : CharacterState.Run
                : CharacterState.Jump);

            movementComponent.Move(moveDirection * Time.fixedDeltaTime);
        }

        public void OnDrawGizmosSelected()
        {
            jumpComponent.TryDrawGizmos();
            attackComponent.TryDrawGizmos();
        }

        protected override void OnEnable()
        {
            _input.Enable();
            attackComponent.Enable();
        }

        protected override void OnDisable()
        {
            _input.Disable();
            attackComponent.Disable();
        }
    }
}