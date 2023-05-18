using Entities.Player;
using UnityEngine;

namespace Buffs
{
    public class DamageResistanceBuff : Buff
    {
        private readonly ParticleSystem _particleInstance;

        public DamageResistanceBuff(PlayerEntity player, BuffData buffData) : base(player, buffData)
        {
            Player.State.DamageResistanceMultipliers.Add(BuffData.buffValue);
            
            if (buffData.particleSpawner != null)
                _particleInstance = BuffData.particleSpawner.Spawn(player.transform);
        }

        protected override void DeBuff()
        {
            if (_particleInstance != null) Object.Destroy(_particleInstance.gameObject);
            
            Player.State.DamageResistanceMultipliers.Remove(BuffData.buffValue);
        }
    }
}