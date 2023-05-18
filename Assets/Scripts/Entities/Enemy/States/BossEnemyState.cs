using DG.Tweening;
using Entities.Enemy.EnemyEntities;
using UnityEngine;

namespace Entities.Enemy.States
{
    [System.Serializable]
    public class BossEnemyState : EntityState
    {
        [Spine.Unity.SpineAnimation]
        [SerializeField] private string deadAnimation;
        
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
            
            _nextTeleportHealth = MaxHealth - _healthPointsToNextStage;
            _boss.Controller.Teleport();
        }

        protected override void EntityDeath()
        {
            _boss.DOKill();
            _boss.enabled = false;
            _boss.SkeletonAnimation.AnimationName = deadAnimation;

            var animation = _boss.SkeletonAnimation.Skeleton.Data.FindAnimation(deadAnimation);
            
            Object.Destroy(_boss.gameObject, animation?.Duration ?? 0);
        }
    }
}