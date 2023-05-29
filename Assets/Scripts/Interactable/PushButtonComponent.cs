using System.Collections.Generic;
using DG.Tweening;
using Entities.Player;
using Interactable.Base;
using UnityEngine;
using Utils;

namespace Interactable
{
    public class PushButtonComponent : InteractableBehaviour<PushButton> {}
    
    [System.Serializable]
    public class PushButton : IInteractable
    {
        [SerializeField] private List<Mechanism> mechanisms;
        [SerializeField] private float undergroundDistance;

        public void Interact(MonoCashed<Collider2D> pushButton, Collider2D other)
        {
            if (!other.TryGetComponent<PlayerEntity>(out _)) return;

            pushButton.transform
                .DOMoveY(pushButton.position.y - undergroundDistance, 0.3f)
                .SetLink(pushButton.gameObject).OnPlay(() =>
                {
                    pushButton.First.Disable();
                    mechanisms.ForEach(mechanism => mechanism.Move());
                });
        }
    }
}