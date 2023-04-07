using System;
using Assets.HeroEditor.Common.CharacterScripts;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Movement : InputMovement
{
    public enum PlayerType { FirstPlayer, SecondPlayer }

    public Character character;
    public new Rigidbody2D rigidbody2D;
    public PlayerType playerInputType;

    private Vector2 _direction = Vector2.zero;
    private bool _isGrounded;
    private void Start() => character.Animator.SetBool("Ready", true);

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) _isGrounded = true;
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground")) _isGrounded = false;
    }

    private void Update()
    {
        _direction = ReadInput(playerInputType);

        UpdateCharacterState();

        if (Input.GetKeyDown(KeyCode.P)) character.SetState(CharacterState.DeathB);
    }

    private void UpdateCharacterState()
    {
        if (_isGrounded) // если на земле
        {
            if (_direction != Vector2.zero) // если двигается
            {
                Turn(_direction.x);
                character.SetState(CharacterState.Run); // бежать
            }
            else if (character.GetState() < CharacterState.DeathB) // если не двигается и не мёртв
            {
                character.SetState(CharacterState.Idle); // бездействовать
            }
        }
        else // если не на земле
        {
            character.SetState(CharacterState.Jump);
        }
    }

    private void FixedUpdate()
    {
        rigidbody2D.MovePosition(rigidbody2D.position + new Vector2(_direction.x, 0.0f));
    }

    private void Turn(float direction)
    {
        character.transform.localScale = new Vector3(Mathf.Sign(direction) * 0.5f, 0.5f, 0.5f);
    }
}

public abstract class InputMovement : MonoBehaviour
{
    private InputActions _input;

    protected Vector2 ReadInput(Movement.PlayerType playerType)
    {
        return playerType switch
        {
            Movement.PlayerType.FirstPlayer => _input.FirstPlayer.Move.ReadValue<Vector2>(),
            Movement.PlayerType.SecondPlayer => _input.SecondPlayer.Move.ReadValue<Vector2>(),
            _ => throw new ArgumentOutOfRangeException(nameof(playerType))
        };
    }

    private void Awake()
    {
        _input = new InputActions();
        OnAwake();
    }

    private void OnEnable()
    {
        _input.Enable();
        OnEnabled();
    }

    private void OnDisable()
    {
        _input.Disable();
        OnDisabled();
    }
    
    protected virtual void OnAwake() {}
    protected virtual void OnEnabled() {}
    protected virtual void OnDisabled() {}
} 