using Entities.Enemy.EnemyEntities;
using Entities.Player;
using Interactable;
using Overlaps2D;
using Spine.Unity;
using UnityEngine;
using Utils;

namespace Entities.Enemy.Controllers
{
    [System.Serializable]
    public class RangedEnemyController : EntityController
    {
        [SerializeField, Min(0.0f)] private float attackDelay = 3.0f;
        [SerializeField, SpineAnimation] private string attackAnimation;
        [SerializeField] private MissileComponent missilePrefab;
        [SerializeField] private CircleOverlap2D attackOverlap;

        private RangedEnemyEntity _enemyEntity;
        private float _attackElapsedTime;

        protected override void Awake<T1, T2>(Entity<T1, T2> entity) => _enemyEntity = entity as RangedEnemyEntity;

        protected override void FixedUpdate()
        {
            _attackElapsedTime += Time.fixedDeltaTime;

            attackOverlap.Perform();
            foreach (var collider in attackOverlap.Colliders)
            {
                if (!collider.TryGetComponent<PlayerEntity>(out var player)) continue;
                _enemyEntity.LookAt(player.transform);

                if (_attackElapsedTime < attackDelay) return;
                _attackElapsedTime = 0.0f;
                _enemyEntity.SkeletonAnimation.AnimationName = attackAnimation;
            }
        }

        public void Attack()
        {
            foreach (var collider in attackOverlap.Colliders)
            {
                if (!collider.TryGetComponent<PlayerEntity>(out var player)) continue;

                var spawnPosition = _enemyEntity.position + Vector3.up;
                var missile = Object.Instantiate(missilePrefab, spawnPosition, Quaternion.identity);
                missile.Move(player.position);
            }

            attackOverlap.Colliders.Clear();
        }

        public virtual void DrawGizmosSelected() => attackOverlap.TryDrawGizmos();
    }
}