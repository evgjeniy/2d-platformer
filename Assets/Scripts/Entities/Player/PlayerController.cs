using Assets.HeroEditor.Common.CharacterScripts;
using Entities.Player.Components;
using InputScripts;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = InputScripts.PlayerInput;

namespace Entities.Player
{
    [System.Serializable]
    public class PlayerController : EntityController
    {
        [SerializeField] private PlayerMovement movementComponent;
        [SerializeField] private PlayerJump jumpComponent;
        [SerializeField] private PlayerAttack attackComponent;

        private PlayerEntity _playerEntity;
        private PlayerInput _input;

        public bool IsGrounded { get; set; }

        public PlayerInputType InputType
        {
            get => _input.PlayerInputType;
            set
            {
                _input.AttackAction.performed -= Attack;
                _input.JumpAction.performed -= jumpComponent.Jump;

                _input.PlayerInputType = value;
                
                _input.AttackAction.performed += Attack;
                _input.JumpAction.performed += jumpComponent.Jump;
            }
        }

        protected override void Awake<T1, T2>(Entity<T1, T2> entity)
        {
            if ((_playerEntity = entity as PlayerEntity) == null) return;

            _input = new PlayerInput();

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

            var moveDirection = _input.MoveDirection;

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