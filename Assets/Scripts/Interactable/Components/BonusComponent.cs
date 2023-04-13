using Interactable.Components.Base;
using UnityEngine;

namespace Interactable.Components
{
    public class BonusComponent : InteractableBehaviour<Bonus>
    {
        private void OnCollisionEnter2D(Collision2D col)
        {
            foreach (var contact in col.contacts)
                if (Mathf.Abs(contact.normal.y - 1.0f) < 0.1f)
                    OnTriggerEnter2D(contact.collider);
        }
    }
}