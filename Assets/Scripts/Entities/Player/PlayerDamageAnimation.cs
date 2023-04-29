using DG.Tweening;
using UnityEngine;

namespace Entities.Player
{
    [System.Serializable]
    public class PlayerDamageAnimation
    {
        [SerializeField, Min(0.0f)] private int ticksAmount = 10;
        [SerializeField, Min(0.0f)] private float oneFadeDuration = 0.2f;
        [SerializeField, Range(0.0f, 1.0f)] private float fadeValue = 0.2f;
        [SerializeField] private Ease fadeEase;
        
        private SpriteRenderer[] _spriteRenderers;
        private Component _context;
        
        public void RegisterContext(Component context)
        {
            _context = context;
            _spriteRenderers = context.GetComponentsInChildren<SpriteRenderer>();
        }

        public void Play(System.Action onPlay = null, System.Action onKill = null)
        {
            if (_spriteRenderers == null) return;

            var sequence = DOTween.Sequence().SetLink(_context.gameObject)
                .OnPlay(() => onPlay?.Invoke()).OnKill(() => onKill?.Invoke());

            foreach (var spriteRenderer in _spriteRenderers)
            {
                var startAlpha = spriteRenderer.color.a;
                sequence.Insert(0, spriteRenderer.DOFade(fadeValue, oneFadeDuration)
                    .SetLoops(ticksAmount, LoopType.Restart).SetEase(fadeEase).OnKill(() =>
                    {
                        var color = spriteRenderer.color;
                        color.a = startAlpha;
                        spriteRenderer.color = color;
                    }).SetLink(spriteRenderer.gameObject));
            }
        }
    }
}