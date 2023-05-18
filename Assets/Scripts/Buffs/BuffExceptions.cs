using Entities.Player;

namespace Buffs
{
    public static class BuffExceptions
    {
        public static Buff GetBuff(this BuffData buffData, PlayerEntity player)
        {
            return buffData.buffType switch
            {
                BuffData.BuffType.AttackMultiplier => new AttackBuff(player, buffData),
                BuffData.BuffType.DamageResistanceMultiplier => new DamageResistanceBuff(player, buffData),
                _ => throw new System.ArgumentOutOfRangeException(nameof(buffData.buffType))
            };
        }
    }
}