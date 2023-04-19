using Assets.HeroEditor.Common.CharacterScripts;
using UnityEngine;

namespace Entities.Player
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerEntity : Entity<PlayerController, PlayerState>
    {
        [field: SerializeField] public Character Character { get; private set; }
        
        public Rigidbody2D Rigidbody { get; private set; }
        
        public PlayerInventory Inventory { get; private set; } = new();

        protected override void EntityAwake() => Rigidbody = GetComponent<Rigidbody2D>();

        private void OnDrawGizmosSelected() => Controller.OnDrawGizmosSelected();
    }
}