using System.Linq;
using Assets.HeroEditor.Common.CharacterScripts;
using HeroEditor.Common.Enums;
using Interactable.Base;
using Overlaps2D;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Entities.Player.PlayerComponents
{
    [System.Serializable]
    public class PlayerAttack
    {
        [SerializeField] private float damage;
        [SerializeField] private AnimationEvents animationEvents;
        [SerializeField] private CircleOverlap2D attackOverlap;
        
        [Header("Events")]
        [SerializeField] private UnityEvent onPlayerAttack;
        [SerializeField] private UnityEvent onPlayerMissAttack;

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

            var isMissAttack = true;
            
            attackOverlap.Perform();
            foreach (var damageable in attackOverlap.Colliders.Select(c => c.GetComponent<IDamageable>()).Where(d => d != null))
            {
                damageable.TakeDamage(damage);
                isMissAttack = false;
            }
            attackOverlap.Colliders.Clear();
            
            if (isMissAttack) onPlayerMissAttack?.Invoke();
            else onPlayerAttack?.Invoke();
        }

        public void TryDrawGizmos() => attackOverlap.TryDrawGizmos();
    }
}