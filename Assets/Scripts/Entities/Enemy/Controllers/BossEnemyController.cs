using System.Collections.Generic;
using Assets.HeroEditor.Common.CommonScripts;
using Entities.Enemy.EnemyEntities;
using Entities.Player;
using Interactable;
using Overlaps2D;
using UnityEngine;
using Utils;

namespace Entities.Enemy.Controllers
{
    [System.Serializable]
    public class BossEnemyController : EntityController
    {
        [Spine.Unity.SpineAnimation]
        [SerializeField] private string attackAnimation;
        [SerializeField, Min(0.0f)] private float attackDelay;
        [SerializeField] private MissileComponent missilePrefab;
        [SerializeField] private CircleOverlap2D attackOverlap;
        [SerializeField] private List<Transform> teleportPoints;

        private BossEnemyEntity _boss;
        private float _attackElapsedTime;

        public int TeleportsCount => teleportPoints.Count; 

        protected override void Awake<T1, T2>(Entity<T1, T2> entity) => _boss = entity as BossEnemyEntity;

        protected override void Start() => Teleport();

        public void Teleport()
        {
            if (teleportPoints.Count == 0) return;
            
            var teleportPoint = teleportPoints.Random();
            _boss.position = teleportPoint.position;
            
            teleportPoints.Remove(teleportPoint);
        }
        
        protected override void FixedUpdate()
        {
            _attackElapsedTime += Time.fixedDeltaTime;

            attackOverlap.Perform();
            foreach (var collider in attackOverlap.Colliders)
            {
                if (!collider.TryGetComponent<PlayerEntity>(out var player)) continue;

                _boss.LookAt(player.transform);

                if (_attackElapsedTime < attackDelay) break;
                _attackElapsedTime = 0.0f;

                _boss.SkeletonAnimation.AnimationName = attackAnimation;
            }
        }

        public void Attack()
        {
            foreach (var collider in attackOverlap.Colliders)
            {
                if (!collider.TryGetComponent<PlayerEntity>(out var player)) continue;

                var spawnPosition = _boss.position + Vector3.up;
                var missile = Object.Instantiate(missilePrefab, spawnPosition, Quaternion.identity);
                missile.Move(player.position);
            }

            attackOverlap.Colliders.Clear();
        }

        public virtual void DrawGizmosSelected() => attackOverlap.TryDrawGizmos();
    }
}