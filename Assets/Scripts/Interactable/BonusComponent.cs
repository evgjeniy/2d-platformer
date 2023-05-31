using System.Collections.Generic;
using Assets.HeroEditor.Common.CommonScripts;
using Buffs;
using Entities.Player;
using Interactable.Base;
using Interactable.InteractableAnimations;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Interactable
{
    public class BonusComponent : InteractableBehaviour<Bonus>
    {
        private void OnCollisionEnter2D(Collision2D col)
        {
            foreach (var contact in col.contacts)
                if (Mathf.Abs(contact.normal.y - 1.0f) < 0.1f)
                    OnTriggerEnter2D(contact.collider);
        }
    }

    [System.Serializable]
    public class Bonus : IInteractable
    {
        [SerializeField] private List<BuffData> buffs;
        [SerializeField] private UnityEvent onBonusCollected;
        
        public void Interact(MonoCashed<Collider2D> bonus, Collider2D other)
        {
            if (!other.TryGetComponent<PlayerEntity>(out var player)) return;

            bonus.PlayBounceJumpAnimationWithFade(onKill: bonus.Destroy, onPlay: () =>
            {
                onBonusCollected?.Invoke();

                bonus.First.Disable();
                player.State.AddBuff(buffs.Random().GetBuff(player));
            });
        }
    }
}