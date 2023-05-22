using Entities.Enemy.EnemyEntities;
using Entities.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Spawners
{
    public class BossLevelPlayerSpawner : PlayerSpawner
    {
        [SerializeField] private BossEnemyEntity boss;
        [SerializeField] private GameObject levelCompleteUi;
        [SerializeField] private UnityEvent onBossDead;

        protected override void SetupLevelCompleteOnBossDeadEvent(PlayerEntity playerEntity)
        {
            if (boss != null)
            {
                boss.State.OnDead += () =>
                {
                    onBossDead?.Invoke();
                    
                    playerEntity.enabled = false;
                    levelCompleteUi.SetActive(true);
                };
            }
        }
    }
}