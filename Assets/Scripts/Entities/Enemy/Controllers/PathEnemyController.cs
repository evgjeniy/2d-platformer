using DG.Tweening;
using UnityEngine;

namespace Entities.Enemy.Controllers
{
    [System.Serializable]
    public class PathEnemyController : EntityController
    {
        [SerializeField] private Waypoint[] waypoints;
        [SerializeField] private LoopType loopType = LoopType.Yoyo;
        
        protected override void Awake<T1, T2>(Entity<T1, T2> entity)
        {
            var sequence = DOTween.Sequence().SetLoops(-1, loopType).SetLink(entity.gameObject);
            
            foreach (var waypoint in waypoints) 
                sequence.Append(
                    entity.transform
                        .DOMove(waypoint.transform.position, waypoint.duration)
                        .SetEase(waypoint.ease)
                );
        }
    }

    [System.Serializable]
    public class Waypoint
    {
        public Transform transform;
        public Ease ease = Ease.Linear;
        [Min(0.0f)] public float duration;
    }
}