using Assets.HeroEditor.Common.CharacterScripts;
using Entities.Player.PlayerComponents;
using InputScripts;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = InputScripts.PlayerInput;

namespace Entities.Player
{
    [System.Serializable]
    public class PlayerController : EntityController
    {
        [field: SerializeField] public PlayerMovement MovementComponent { get; private set; }
        [field: SerializeField] public PlayerJump JumpComponent { get; private set; }
        [field: SerializeField] public PlayerAttack AttackComponent { get; private set; }

        private PlayerEntity _playerEntity;
        private PlayerInput _input;

        public PlayerInputType InputType
        {
            get => _input.PlayerInputType;
            set
            {
                _input.AttackAction.performed -= Attack;
                _input.PlayerInputType = value;
                _input.AttackAction.performed += Attack;
            }
        }

        private void Attack(InputAction.CallbackContext callbackContext) => _playerEntity.Character.Slash();

        protected override void Awake<T1, T2>(Entity<T1, T2> entity)
        {
            if ((_playerEntity = entity as PlayerEntity) == null) return;

            _input = new PlayerInput();

            MovementComponent.RegisterContext(_playerEntity);
            JumpComponent.RegisterContext(_playerEntity);
        }

        protected override void FixedUpdate()
        {
            JumpComponent.CheckJumpState();

            var moveDirection = _input.MoveDirection;
            
            _playerEntity.Character.SetState(JumpComponent.JumpState switch
            {
                PlayerJump.State.Air => CharacterState.Jump,
                PlayerJump.State.Ladder => CharacterState.Climb,
                PlayerJump.State.Grounded => moveDirection.x == 0.0f ? CharacterState.Idle : CharacterState.Run,
                _ => throw new System.ArgumentOutOfRangeException(nameof(JumpComponent.JumpState)) 
            });

            MovementComponent.Move(moveDirection.x);
            JumpComponent.TryJump(moveDirection.y);
        }

        protected override void OnEnable()
        {
            _input.Enable();
            AttackComponent.Enable();
        }

        protected override void OnDisable()
        {
            _input.Disable();
            AttackComponent.Disable();
        }

        public void OnDrawGizmosSelected()
        {
            JumpComponent.TryDrawGizmos();
            AttackComponent.TryDrawGizmos();
        }
    }
}