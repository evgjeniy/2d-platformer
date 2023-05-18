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
        [Spine.Unity.SpineAnimation]
        [SerializeField] private string attackAnimation;
        [SerializeField, Min(0.0f)] private float attackDelay;
        [SerializeField] private float damage;
        [SerializeField] private CircleOverlap2D attackOverlap;

        private MeleeEnemyEntity _enemyEntity;
        private float _attackElapsedTime;

        protected override void Awake<T1, T2>(Entity<T1, T2> entity) => _enemyEntity = entity as MeleeEnemyEntity;

        protected override void FixedUpdate()
        {
            _attackElapsedTime += Time.fixedDeltaTime;

            attackOverlap.Perform();
            foreach (var collider in attackOverlap.Colliders)
            {
                if (!collider.TryGetComponent<PlayerEntity>(out var player)) continue;

                _enemyEntity.LookAt(player.transform);

                if (_attackElapsedTime < attackDelay) break;
                _attackElapsedTime = 0.0f;

                _enemyEntity.SkeletonAnimation.AnimationName = attackAnimation;
            }
        }

        public void Attack()
        {
            foreach (var collider in attackOverlap.Colliders)
                if (collider.TryGetComponent<PlayerEntity>(out var player))
                    player.TakeDamage(damage);

            attackOverlap.Colliders.Clear();
        }

        public virtual void DrawGizmosSelected() => attackOverlap.TryDrawGizmos();
    }
}