﻿using DG.Tweening;
using UnityEngine;

namespace Interactable
{
    public class MissileComponent : EnemyInteractableComponent
    {
        [SerializeField, Min(0.0f)] private float speed = 1.0f;
        
        public void Move(Vector3 targetPosition)
        {
            var deltaDistance = targetPosition - transform.position;
 
            transform.DOMove(targetPosition, deltaDistance.magnitude / speed)
                .SetLink(gameObject).SetEase(Ease.OutSine).OnKill(() => Destroy(gameObject));
        } 
    }
}