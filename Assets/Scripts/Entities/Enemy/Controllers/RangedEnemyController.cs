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

        protected RangedEnemyEntity EnemyEntity;
        private float _attackElapsedTime;

        protected override void Awake<T1, T2>(Entity<T1, T2> entity)
        {
            if ((EnemyEntity = entity as RangedEnemyEntity) == null) return;
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
            {
                if (!collider.TryGetComponent<PlayerEntity>(out var player)) continue;
                
                var missile = Object.Instantiate(missilePrefab, EnemyEntity.transform.position + Vector3.up, Quaternion.identity);
                missile.Move(player.transform.position);
            }

            attackOverlap.Colliders.Clear();
        }

        public virtual void DrawGizmosSelected() => attackOverlap.TryDrawGizmos();
    }
}