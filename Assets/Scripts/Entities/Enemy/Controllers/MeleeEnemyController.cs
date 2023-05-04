using Entities.Enemy.EnemyEntities;
using Entities.Player;
using Overlaps2D;
using UnityEngine;
using Utils;

namespace Entities.Enemy.Controllers
{
    [System.Serializable]
    public class MeleeEnemyController : EntityController
    {
        [SerializeField, Spine.Unity.SpineAnimation] private string attackAnimation;
        [SerializeField] private float attackDelay;
        [SerializeField] private float damage;
        [SerializeField] private CircleOverlap2D attackOverlap;

        protected MeleeEnemyEntity EnemyEntity;
        private float _attackElapsedTime;

        protected override void Awake<T1, T2>(Entity<T1, T2> entity)
        {
            if ((EnemyEntity = entity as MeleeEnemyEntity) == null) return;
            Debug.Log($"MeleeEnemyController.Awake => EnemyEntity is {EnemyEntity}");
        }

        protected override void FixedUpdate()
        {
            _attackElapsedTime += Time.fixedDeltaTime;
            if (_attackElapsedTime < attackDelay) return;
            _attackElapsedTime = 0.0f;
            
            attackOverlap.Perform();
            foreach (var collider in attackOverlap.Colliders)
            {
                if (!collider.TryGetComponent<PlayerEntity>(out var player)) continue;
                
                EnemyEntity.SkeletonAnimation.AnimationName = attackAnimation;
                EnemyEntity.LookAt(player.transform);
            }
        }

        public void Attack()
        {
            foreach (var collider in attackOverlap.Colliders)
                if (collider.TryGetComponent<PlayerEntity>(out var player))
                    player.State.TakeDamage(damage);
            
            attackOverlap.Colliders.Clear();
        }

        public virtual void DrawGizmosSelected() => attackOverlap.TryDrawGizmos();
    }
}