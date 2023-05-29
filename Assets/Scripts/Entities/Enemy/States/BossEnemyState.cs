using DG.Tweening;
using Entities.Enemy.EnemyEntities;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Entities.Enemy.States
{
    [System.Serializable]
    public class BossEnemyState : EntityState
    {
        [Spine.Unity.SpineAnimation]
        [SerializeField] private string deadAnimation;
        [SerializeField] private UnityEvent<Vector3> preTeleport;
        
        private BossEnemyEntity _boss;
        private float _nextTeleportHealth;
        private float _healthPointsToNextStage;

        protected override void Awake<T1, T2>(Entity<T1, T2> entity)
        {
            if ((_boss = entity as BossEnemyEntity) == null) return;
            CurrentHealth = MaxHealth;

            _healthPointsToNextStage = MaxHealth / _boss.Controller.TeleportsCount;
            _nextTeleportHealth = MaxHealth - _healthPointsToNextStage;
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);

            if (CurrentHealth > _nextTeleportHealth) return;
            
            preTeleport?.Invoke(_boss.position + Vector3.up * 2);
            
            _nextTeleportHealth -= _healthPointsToNextStage;
            _boss.Controller.Teleport();
        }

        protected override void EntityDeath()
        {
            _boss.DOKill();
            _boss.enabled = false;
            _boss.SkeletonAnimation.AnimationName = deadAnimation;

            var animation = _boss.SkeletonAnimation.Skeleton.Data.FindAnimation(deadAnimation);
            
            _boss.Destroy(animation?.Duration ?? 0);
        }
    }
}