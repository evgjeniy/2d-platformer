using DG.Tweening;
using Entities.Enemy.EnemyEntities;
using UnityEngine;

namespace Entities.Enemy.States
{
    [System.Serializable]
    public class RangedEnemyState : EntityState
    {
        [SerializeField] private float deathDelay = 1.0f;
        
        private RangedEnemyEntity _enemy;

        protected override void Awake<T1, T2>(Entity<T1, T2> entity)
        {
            if ((_enemy = entity as RangedEnemyEntity) == null) return;

            CurrentHealth = MaxHealth;
        }
 
        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            Debug.Log($"Melee Enemy is damaged: {damage}");
        }

        protected override void EntityDeath()
        {
            _enemy.DOKill();
            _enemy.enabled = false;
            _enemy.SkeletonAnimation.AnimationName = "Dead";
            
            Object.Destroy(_enemy.gameObject, deathDelay);
        }
    }
}