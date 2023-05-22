using Assets.HeroEditor.Common.CharacterScripts;
using Entities.Player.PlayerComponents;
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
        
        protected override void EntityEnable() => Rigidbody.bodyType = RigidbodyType2D.Dynamic;
        
        protected override void EntityDisable()
        {
            Character.SetState(CharacterState.Idle);
            Rigidbody.bodyType = RigidbodyType2D.Static;
        }

        private void OnDrawGizmosSelected() => Controller.OnDrawGizmosSelected();
    }
}