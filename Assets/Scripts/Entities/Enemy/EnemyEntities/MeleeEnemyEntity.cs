﻿using Entities.Enemy.Controllers;
using Entities.Enemy.States;
using Spine.Unity;
using UnityEngine;

namespace Entities.Enemy.EnemyEntities
{
    [RequireComponent(typeof(SkeletonAnimation))]
    public class MeleeEnemyEntity : Entity<MeleeEnemyController, MeleeEnemyState>
    {
        public SkeletonAnimation SkeletonAnimation { get; private set; }

        protected override void EntityAwake() => SkeletonAnimation = GetComponent<SkeletonAnimation>();

        public void EntityAttack() => Controller.Attack();
        
        private void OnDrawGizmosSelected() => Controller.DrawGizmosSelected();   
    }
}