using System;
using System.Collections;
using UnityEngine;

namespace Movement.SurfaceMovement.Jump
{
    public class PureAnimation
    {
        public TransformChanges2D LastChange { get; set; }

        private Coroutine _lastAnimation;
        private readonly MonoBehaviour _context;

        public PureAnimation(MonoBehaviour context) => _context = context;

        public void Play(float duration, Func<float, TransformChanges2D> body)
        {
            if (_lastAnimation != null)
                _context.StopCoroutine(_lastAnimation);

            _lastAnimation = _context.StartCoroutine(GetAnimation(duration, body));
        }

        private IEnumerator GetAnimation(float duration, Func<float, TransformChanges2D> body)
        {
            for (var progress = 0.0f; progress < 1.0f; progress += Time.deltaTime / duration)
            {
                LastChange = body.Invoke(progress);
                yield return null;
            }
        }
    }
}