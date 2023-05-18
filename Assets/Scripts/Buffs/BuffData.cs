using Spawners;
using UnityEngine;

namespace Buffs
{
    [System.Serializable]
    public struct BuffData
    {
        public BuffType buffType;
        [Min(0.0f)] public float buffTime;
        [Min(0.0f)] public float buffValue;
        public ParticleSpawner particleSpawner;

        public enum BuffType { AttackMultiplier, DamageResistanceMultiplier }
    }
}