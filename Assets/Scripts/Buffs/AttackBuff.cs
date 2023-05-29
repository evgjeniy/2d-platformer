using Entities.Player;
using UnityEngine;
using Utils;

namespace Buffs
{
    public class AttackBuff : Buff
    {
        private ParticleSystem _particleInstance;
        
        public AttackBuff(PlayerEntity player, BuffData buffData) : base(player, buffData)
        {
            Player.Controller.AttackComponent.Damage *= 1 + BuffData.buffValue;
            BuffData.particleSpawner.IfNotNull(spawner => _particleInstance = spawner.Spawn(player.transform));
        }

        protected override void DeBuff()
        {
            _particleInstance.IfNotNull(particle => particle.Destroy());
            Player.Controller.AttackComponent.Damage /= 1 + BuffData.buffValue;
        }
    }
}