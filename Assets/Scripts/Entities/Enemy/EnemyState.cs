using DG.Tweening;
using UnityEngine;

namespace Entities.Enemy
{
    [System.Serializable]
    public class EnemyState : EntityState
    {
        private EnemyEntity _enemy;

        protected override void Awake<T1, T2>(Entity<T1, T2> entity)
        {
            if ((_enemy = entity as EnemyEntity) == null) return;
        }

        protected override void EntityDeath()
        {
            Debug.Log($"ENEMY ({_enemy.name}) IS DEAD");

            _enemy.DOKill(true);
            _enemy.enabled = false;
            
            Object.Destroy(_enemy.gameObject, 1.0f);
        }
    }
}