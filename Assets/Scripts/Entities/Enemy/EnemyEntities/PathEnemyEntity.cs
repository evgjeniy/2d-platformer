using Entities.Enemy.Controllers;
using Entities.Enemy.States;
using Spine.Unity;

namespace Entities.Enemy.EnemyEntities
{
    public class PathEnemyEntity : Entity<PathEnemyController, PathEnemyState>
    {
        public SkeletonAnimation SkeletonAnimation { get; private set; }

        protected override void EntityAwake() => SkeletonAnimation = GetComponent<SkeletonAnimation>();
    }
}