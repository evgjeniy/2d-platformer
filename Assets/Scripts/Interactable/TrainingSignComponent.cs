using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Entities.Player;
using Interactable.Base;
using UnityEngine;

namespace Interactable
{
    public class TrainingSignComponent : InteractableBehaviour<TrainingSign>
    {
        private void OnTriggerExit2D(Collider2D other) => interactable.Interact(this, other);
    }
    
    [System.Serializable]
    public class TrainingSign : IInteractable
    {
        [SerializeField] private SpriteRenderer fadeTraining;

        private TweenerCore<Color, Color, ColorOptions> _tween;
        private List<PlayerEntity> _enteredEntities = new();

        public void Interact(MonoCashed<Collider2D> sign, Collider2D other)
        {
            if (!other.TryGetComponent<PlayerEntity>(out var playerEntity)) return;

            if (sign.First.IsTouching(other)) _enteredEntities.Add(playerEntity);
            else _enteredEntities.Remove(playerEntity);
            
            _tween?.Kill();
            _tween = fadeTraining.DOFade(_enteredEntities.Count == 0 ? 0.0f : 1.0f, 0.5f).SetEase(Ease.Linear)
                .SetLink(fadeTraining.gameObject).OnKill(() => _tween = null);
        }
    }
}