using DG.Tweening;
using UnityEngine;

namespace Interactable.Base
{
    public abstract class MovableInteractableBehaviour<T> : InteractableBehaviour<T> where T : IInteractable, new()
    {
        [Header("Movable Settings")]
        [SerializeField] private Vector2 moveOffset = new(0.0f, 0.2f);
        [SerializeField, Range(0.5f, 5.0f)] private float moveDuration = 1.0f;
        [SerializeField, Range(0.0f, 0.5f)] private float durationRandomOffset = 0.3f;
        [SerializeField] private Ease moveEase = Ease.InOutSine;
        
        protected override void PostAwake()
        {
            var duration = Random.Range(moveDuration - durationRandomOffset, moveDuration + durationRandomOffset);
            DOTween.Sequence()
                .Append(transform.DOMove(transform.position + (Vector3) moveOffset, duration).SetEase(moveEase))
                .SetLoops(-1, LoopType.Yoyo).SetLink(gameObject).Play();
        }
    }
}