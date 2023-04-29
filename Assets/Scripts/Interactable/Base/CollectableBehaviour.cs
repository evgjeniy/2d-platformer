using DG.Tweening;
using UnityEngine;

namespace Interactable.Base
{
    [System.Serializable]
    public abstract class CollectableBehaviour
    {
        [Header("Fade off Settings")]
        [SerializeField] private float fadeOffDuration = 0.3f;
        [SerializeField] private Ease fadeOffEase = Ease.InExpo;
        
        [Header("Collectable Animation Settings")]
        [SerializeField] private float yMoveOffset = 1.5f;
        [SerializeField] private float yMoveDuration = 0.5f;
        [SerializeField] private Ease yMoveEase = Ease.OutExpo;
        
        public virtual void PlayCollectAnimation(MonoTransform collectable, System.Action onPlay = null, System.Action onKill = null)
        {
            collectable.GetComponent<SpriteRenderer>()?.DOFade(0.0f, fadeOffDuration)
                .SetEase(fadeOffEase).SetLink(collectable.gameObject);
            
            collectable.transform.DOMoveY(collectable.transform.position.y + yMoveOffset, yMoveDuration)
                .SetEase(yMoveEase).SetLink(collectable.gameObject)
                .OnPlay(() => onPlay?.Invoke())
                .OnKill(() => onKill?.Invoke());
        }
    }
}