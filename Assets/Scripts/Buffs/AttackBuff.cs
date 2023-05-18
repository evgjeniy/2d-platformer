using Entities.Player;
using UnityEngine;

namespace Buffs
{
    public class AttackBuff : Buff
    {
        private readonly ParticleSystem _particleInstance;
        
        public AttackBuff(PlayerEntity player, BuffData buffData) : base(player, buffData)
        {
            Player.Controller.AttackComponent.Damage *= 1 + BuffData.buffValue;
            
            if (buffData.particleSpawner != null) 
                _particleInstance = BuffData.particleSpawner.Spawn(player.transform);
        }

        protected override void DeBuff()
        {
            if (_particleInstance != null) Object.Destroy(_particleInstance.gameObject);
            
            Player.Controller.AttackComponent.Damage /= 1 + BuffData.buffValue;
        }
    }
}