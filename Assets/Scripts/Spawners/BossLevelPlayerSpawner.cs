using Entities.Enemy.EnemyEntities;
using Entities.Player;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Spawners
{
    public class BossLevelPlayerSpawner : PlayerSpawner
    {
        [SerializeField] private BossEnemyEntity boss;
        [SerializeField] private UnityEvent onBossDead;

        protected override void SetupLevelCompleteOnBossDeadEvent(PlayerEntity playerEntity) =>
            boss.IfNotNull(bossEntity => bossEntity.State.OnDead += () =>
            {
                playerEntity.Disable();
                onBossDead?.Invoke();
            });
    }
}