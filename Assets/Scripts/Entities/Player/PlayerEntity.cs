using InputScripts;
using UnityEngine;

namespace Entities.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(IInputBehaviour))]
    public class PlayerEntity : Entity<PlayerController, PlayerState>
    {
        [Header("State Changes Listeners")]
        public CharacterStateEvent onStateChangedEvent;

        public PlayerInventory Inventory { get; private set; } = new();

        protected override void EntityAwake() => onStateChangedEvent ??= new CharacterStateEvent();

        private void OnDrawGizmosSelected() => Controller.OnDrawGizmosSelected();
    }
}