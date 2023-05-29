using Entities.Player;
using UnityEngine;
using Utils;

namespace Buffs
{
    public class DamageResistanceBuff : Buff
    {
        private ParticleSystem _particleInstance;

        public DamageResistanceBuff(PlayerEntity player, BuffData buffData) : base(player, buffData)
        {
            Player.State.DamageResistanceMultipliers.Add(BuffData.buffValue);
            BuffData.particleSpawner.IfNotNull(spawner => _particleInstance = spawner.Spawn(player.transform));
        }

        protected override void DeBuff()
        {
            _particleInstance.IfNotNull(particle => particle.Destroy());
            Player.State.DamageResistanceMultipliers.Remove(BuffData.buffValue);
        }
    }
}