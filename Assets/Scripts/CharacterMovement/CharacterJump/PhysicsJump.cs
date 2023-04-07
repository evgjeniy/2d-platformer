using System;
using UnityEngine;

namespace CharacterMovement.CharacterJump
{
    public class PhysicsJump : MonoTransform
    {
        [SerializeField] private new Rigidbody2D rigidbody2D;
        [SerializeField] private SurfaceSlider surfaceSlider;
        [SerializeField] private JumpFX jumpFX;
        [SerializeField] private float length;
        [SerializeField] private float duration;

        private PureAnimation _playTime;

        protected override void PostAwake() => _playTime = new PureAnimation(this);

        public void Jump(Vector2 direction)
        {
            var target = (Vector2)transform.position + (direction * length);
            var startPosition = transform.position;
            var fxPlayTime = jumpFX.PlayAnimations(transform, duration);
            
            _playTime.Play(duration, (progress) =>
            {
                transform.position = Vector2.Lerp(startPosition, target, progress) + fxPlayTime.LastChange.Position;
                transform.localScale = fxPlayTime.LastChange.Scale;
                return null;
            });
        }
    }
}