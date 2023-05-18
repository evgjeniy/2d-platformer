using System.Linq;
using Assets.HeroEditor.Common.CharacterScripts;
using Interactable.Base;
using Overlaps2D;
using UnityEngine;

namespace Entities.Player.PlayerComponents
{
    [System.Serializable]
    public class PlayerAttack
    {
        [SerializeField] private float damage;
        [SerializeField] private AnimationEvents animationEvents;
        [SerializeField] private CircleOverlap2D attackOverlap;

        public float Damage
        {
            get => damage;
            set
            {
                damage = value;
                if (damage < 0.0f) damage = 0.0f;
            }
        }

        public void Enable() => animationEvents.OnCustomEvent += OnAnimationEvent;
        
        public void Disable() => animationEvents.OnCustomEvent -= OnAnimationEvent;
        
        private void OnAnimationEvent(string eventName)
        {
            if (eventName != "Hit") return;
            
            attackOverlap.Perform();
            foreach (var damageable in attackOverlap.Colliders.Select(col => col.GetComponent<IDamageable>()).Where(damageable => damageable != null))
            {
                damageable.TakeDamage(damage);
            }

            attackOverlap.Colliders.Clear();
        }

        public void TryDrawGizmos() => attackOverlap.TryDrawGizmos();
    }
}