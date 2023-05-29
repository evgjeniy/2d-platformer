using DG.Tweening;
using Entities.Enemy.EnemyEntities;
using UnityEngine;
using Utils;

namespace Entities.Enemy.States
{
    [System.Serializable]
    public class MeleeEnemyState : EntityState
    {
        [Spine.Unity.SpineAnimation]
        [SerializeField] private string deadAnimation;
        
        private MeleeEnemyEntity _enemy;

        protected override void Awake<T1, T2>(Entity<T1, T2> entity)
        {
            if ((_enemy = entity as MeleeEnemyEntity) == null) return;
            CurrentHealth = MaxHealth;
        }

        protected override void EntityDeath()
        {
            _enemy.DOKill();
            _enemy.enabled = false;
            _enemy.SkeletonAnimation.AnimationName = deadAnimation;

            var animation = _enemy.SkeletonAnimation.Skeleton.Data.FindAnimation(deadAnimation);
            
            _enemy.Destroy(animation?.Duration ?? 0);
        }
    }
}