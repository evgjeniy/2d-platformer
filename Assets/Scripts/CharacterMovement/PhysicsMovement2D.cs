using Assets.HeroEditor.Common.CharacterScripts;
using UnityEngine;

namespace CharacterMovement
{
    [RequireComponent(typeof(IInputBehaviour))]
    public class PhysicsMovement2D : MonoBehaviour
    {
        [SerializeField] private Character character;
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private SurfaceSlider surfaceSlider;
        [SerializeField] private float speed;

        private IInputBehaviour _input;
        private Vector2 _direction;

        private void Awake() => _input = GetComponent<IInputBehaviour>();
        
        private void Update()
        {
            _direction = _input.GetMoveDirection();
            
            UpdateCharacterState();

            if (Input.GetKeyDown(KeyCode.P)) character.SetState(CharacterState.DeathB);
        }

        private void FixedUpdate()
        {
            var directionAlonSurface = surfaceSlider.Project(_direction.normalized);
            var offset = directionAlonSurface * (speed * Time.fixedDeltaTime);
            
            rigidbody.MovePosition(rigidbody.position + offset);
        }
        
        private void UpdateCharacterState()
        {
            if (surfaceSlider.IsGrounded)
            {
                if (_direction != Vector2.zero)
                {
                    Turn(_direction.x);
                    character.SetState(CharacterState.Run);
                }
                else if (character.GetState() < CharacterState.DeathB)
                {
                    character.SetState(CharacterState.Idle);
                }
            }
            else character.SetState(CharacterState.Jump);
        }
        
        private void Turn(float direction)
        {
            character.transform.localScale = new Vector3(Mathf.Sign(direction) * 0.5f, 0.5f, 0.5f);
        }
    }
}