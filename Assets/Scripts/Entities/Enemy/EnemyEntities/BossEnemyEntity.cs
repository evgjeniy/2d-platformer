using Entities.Enemy.Controllers;
using Entities.Enemy.States;
using Spine.Unity;

namespace Entities.Enemy.EnemyEntities
{
    public class BossEnemyEntity : Entity<BossEnemyController, BossEnemyState>
    {
        public SkeletonAnimation SkeletonAnimation { get; private set; }
        
        protected override void EntityAwake() => SkeletonAnimation = GetComponent<SkeletonAnimation>();

        public void EntityAttack() => Controller.Attack();
        
        private void OnDrawGizmosSelected() => Controller.DrawGizmosSelected();   
    }
}