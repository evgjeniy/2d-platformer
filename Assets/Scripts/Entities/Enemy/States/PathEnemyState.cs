using DG.Tweening;
using Entities.Enemy.EnemyEntities;
using UnityEngine;

namespace Entities.Enemy.States
{
    [System.Serializable]
    public class PathEnemyState : EntityState
    {
        [Spine.Unity.SpineAnimation]
        [SerializeField] private string deadAnimation;
        
        private PathEnemyEntity _enemy;

        protected override void Awake<T1, T2>(Entity<T1, T2> entity)
        {
            if ((_enemy = entity as PathEnemyEntity) == null) return;
            CurrentHealth = MaxHealth;
        }

        protected override void EntityDeath()
        {
            _enemy.DOKill();
            _enemy.enabled = false;
            _enemy.SkeletonAnimation.AnimationName = deadAnimation;

            var animation = _enemy.SkeletonAnimation.Skeleton.Data.FindAnimation(deadAnimation);
            
            Object.Destroy(_enemy.gameObject, animation?.Duration ?? 0);
        }
    }
}