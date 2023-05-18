using System.Collections.Generic;
using DG.Tweening;
using Entities.Player;
using Interactable.Base;
using UnityEngine;

namespace Interactable
{
    public class LeverComponent : InteractableBehaviour<Lever> {}

    [System.Serializable]
    public class Lever : IInteractable
    {
        [SerializeField] private List<Mechanism> mechanisms;
        
        public void Interact(MonoCashed<Collider2D> lever, Collider2D other)
        {
            if (!other.TryGetComponent<PlayerEntity>(out var player)) return;
            
            var scaleValue = lever.localScale.x;
            
            lever.transform
                .DOScaleX( Mathf.Abs(scaleValue) * -Mathf.Sign(scaleValue), 0.1f)
                .SetLink(lever.gameObject).OnPlay(() =>
                {
                    lever.First.enabled = false;
                    mechanisms.ForEach(mechanism => mechanism.Move());
                });
        }
    }
}