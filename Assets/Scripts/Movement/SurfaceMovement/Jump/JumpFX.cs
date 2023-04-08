using UnityEngine;

namespace Movement.SurfaceMovement.Jump
{
    public class JumpFX : MonoBehaviour
    {
        [SerializeField] private AnimationCurve yAnimation;
        [SerializeField] private AnimationCurve scaleAnimation;
        [SerializeField] private float height;
        
        private PureAnimation _playTime;

        private void Awake() => _playTime = new PureAnimation(this);

        public PureAnimation PlayAnimations(Transform jumper, float duration)
        {
            _playTime.Play(duration, progress =>
            {
                var position = Vector2.Scale(new Vector2(0.0f, height * yAnimation.Evaluate(progress)), jumper.up);
                var scale = Vector2.one * scaleAnimation.Evaluate(progress);

                return new TransformChanges2D { Position = position, Scale = scale };
            });
            
            return _playTime;
        }
    }
}