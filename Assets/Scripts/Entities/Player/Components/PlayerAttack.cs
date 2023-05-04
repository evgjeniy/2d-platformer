using Assets.HeroEditor.Common.CharacterScripts;
using Entities.Enemy.EnemyEntities;
using Overlaps2D;
using UnityEngine;

namespace Entities.Player.Components
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
                hitCollider.GetComponent<MeleeEnemyEntity>()?.State.TakeDamage(damage);
                hitCollider.GetComponent<PathEnemyEntity>()?.State.TakeDamage(damage);
                hitCollider.GetComponent<RangedEnemyEntity>()?.State.TakeDamage(damage);
            }
            attackOverlap.Colliders.Clear();
        }

        public void TryDrawGizmos() => attackOverlap.TryDrawGizmos();
    }
}