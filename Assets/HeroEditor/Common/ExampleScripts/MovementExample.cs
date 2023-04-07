using Assets.HeroEditor.Common.CharacterScripts;
using UnityEngine;

namespace Assets.HeroEditor.Common.ExampleScripts
{
    /// <summary>
    /// Character move and jump example. Built-in component CharacterController (3D) is used. It can be replaced by 2D colliders.
    /// </summary>
    public class MovementExample : MonoBehaviour
    {
        public Character Character;
        public CharacterController Controller; // https://docs.unity3d.com/ScriptReference/CharacterController.html

        private Vector3 _speed = Vector3.zero;
        private InputActions _input;

        private void Awake() => _input = new InputActions();
        private void OnEnable() => _input.Enable();
        private void OnDisable() => _input.Disable();

        private void Start()
        {
            if (Controller == null)
            {
                Controller = Character.gameObject.AddComponent<CharacterController>();
                Controller.center = new Vector3(0, 1.125f);
                Controller.height = 2.5f;
                Controller.radius = 0.75f;
                Controller.minMoveDistance = 0;
            }

            Character.Animator.SetBool("Ready", true);
        }

        private void Update()
        {
            var direction = _input.SecondPlayer.Move.ReadValue<Vector2>();

            Move(direction);

            if (Input.GetKeyDown(KeyCode.P)) Character.SetState(CharacterState.DeathB);
        }

        private void Move(Vector2 direction)
        {
            if (Controller.isGrounded)
            {
                _speed = new Vector3(5 * direction.x, 10 * direction.y);

                if (direction != Vector2.zero)
                {
                    Turn(_speed.x);
                    Character.SetState(CharacterState.Run);
                }
                else if (Character.GetState() < CharacterState.DeathB)
                {
                    Character.SetState(CharacterState.Idle);
                }
            }
            else
            {
                Character.SetState(CharacterState.Jump);
            }

            _speed.y -= 25 * Time.deltaTime; // Depends on project physics settings
            Controller.Move(_speed * Time.deltaTime);
        }

        private void Turn(float direction)
        {
            Character.transform.localScale = new Vector3(Mathf.Sign(direction), 1, 1);
        }
    }
}