using Assets.HeroEditor.Common.CharacterScripts;
using Entities.Enemy;
using Overlaps2D;
using UnityEngine;

namespace Entities.Player
{
    [System.Serializable]
    public class PlayerAttack
    {
        [SerializeField] private float damage;
        [SerializeField] private AnimationEvents animationEvents;
        [SerializeField] private CircleOverlap2D attackOverlap;

        public void Enable() => animationEvents.OnCustomEvent += OnAnimationEvent;
        
        public void Disable() => animationEvents.OnCustomEvent -= OnAnimationEvent;
        
        private void OnAnimationEvent(string eventName)
        {
            if (eventName != "Hit") return;
            
            attackOverlap.Perform();
            foreach (var hitCollider in attackOverlap.Colliders)
            {
                if (!hitCollider.TryGetComponent<EnemyEntity>(out var enemy)) continue;
                
                enemy.State.TakeDamage(damage);
            }
        }

        public void TryDrawGizmos() => attackOverlap.TryDrawGizmos();
    }
}