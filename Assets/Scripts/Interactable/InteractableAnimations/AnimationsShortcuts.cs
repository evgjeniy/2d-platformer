using System;
using DG.Tweening;
using UnityEngine;

namespace Interactable.InteractableAnimations
{
    public static class AnimationsShortcuts
    {
        public static void PlayBounceJumpAnimationWithFade(this MonoTransform gameObject, float yOffset = 1.0f, Action onPlay = null, Action onKill = null) =>
            PlayBounceJumpAnimationWithFade(gameObject.transform, yOffset, onPlay, onKill);

        public static void PlayBounceJumpAnimationWithFade(this Transform gameObject, float yOffset = 1.0f, Action onPlay = null, Action onKill = null)
        {
            var sequence = CreateBounceJumpSequence(gameObject, yOffset, onPlay, onKill);
            
            var sprite = gameObject.GetComponent<SpriteRenderer>();
            if (sprite != null) 
                sequence.Insert(0, gameObject.GetComponent<SpriteRenderer>()?.DOFade(0.0f, 0.7f).SetEase(Ease.InExpo));
        }
        
        public static void PlayBounceJumpAnimation(this MonoTransform gameObject, float yOffset = 1.0f, Action onPlay = null, Action onKill = null) =>
            PlayBounceJumpAnimation(gameObject.transform, yOffset, onPlay, onKill);

        public static void PlayBounceJumpAnimation(this Transform gameObject, float yOffset = 1.0f, Action onPlay = null, Action onKill = null) =>
            CreateBounceJumpSequence(gameObject, yOffset, onPlay, onKill);

        private static Sequence CreateBounceJumpSequence(Transform gameObject, float yOffset, Action onPlay, Action onKill) => 
            DOTween.Sequence().SetLink(gameObject.gameObject)
                .OnPlay(() => onPlay?.Invoke()).OnKill(() => onKill?.Invoke())
                .Append(gameObject.DOMoveY(gameObject.position.y + yOffset, 0.1f).SetEase(Ease.OutExpo))
                .Append(gameObject.DOMoveY(gameObject.position.y, 0.4f).SetEase(Ease.OutBounce));
    }
}