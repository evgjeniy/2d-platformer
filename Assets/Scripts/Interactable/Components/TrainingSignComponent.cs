using Interactable.Components.Base;
using UnityEngine;

namespace Interactable.Components
{
    public class TrainingSignComponent : InteractableBehaviour<TrainingSign>
    {
        private void OnTriggerExit2D(Collider2D other) => interactable.Interact(this, other);
    }
}