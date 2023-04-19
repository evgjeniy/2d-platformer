using System.Collections.Generic;
using Assets.HeroEditor.Common.CommonScripts;
using DG.Tweening;
using Entities.Player;
using UnityEngine;

namespace Interactable
{
    [System.Serializable]
    public class Bonus : IInteractable
    {
        [SerializeField] private List<string> boosters; // temp -> List<Boost>
        
        public void Interact(MonoCashed<Collider2D> bonus, Collider2D other)
        {
            if (!other.TryGetComponent<PlayerEntity>(out var player)) return;
            
            bonus.First.enabled = false;
            
            Debug.Log($"{player.name} collect bonus: {boosters.Random()}"); // TODO - add bonus to the player

            PlayInteractionAnimation(bonus.transform);
        }

        private static void PlayInteractionAnimation(Transform bonus)
        {
            var sprite = bonus.GetComponent<SpriteRenderer>();

            var sequence = DOTween.Sequence().SetLink(bonus.gameObject).OnKill(() => Object.Destroy(bonus.gameObject))
                .Append(bonus.DOMoveY(bonus.position.y + 1.0f, 0.1f).SetEase(Ease.OutExpo))
                .Append(bonus.DOMoveY(bonus.position.y, 0.4f).SetEase(Ease.OutBounce));
                
            if (sprite != null) 
                sequence.Insert(0, bonus.GetComponent<SpriteRenderer>()?.DOFade(0.0f, 0.7f).SetEase(Ease.InExpo));
        }
    }
}