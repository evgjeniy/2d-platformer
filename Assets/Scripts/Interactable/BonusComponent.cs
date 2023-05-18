using System.Collections.Generic;
using Assets.HeroEditor.Common.CommonScripts;
using Buffs;
using DG.Tweening;
using Entities.Player;
using Interactable.Base;
using UnityEngine;
using Object = UnityEngine.Object;

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
        
        public void Interact(MonoCashed<Collider2D> bonus, Collider2D other)
        {
            if (!other.TryGetComponent<PlayerEntity>(out var player)) return;
            
            bonus.First.enabled = false;
            player.State.AddBuff(buffs.Random().GetBuff(player));
            
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