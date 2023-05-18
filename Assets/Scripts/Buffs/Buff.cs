using Entities.Player;

namespace Buffs
{
    public abstract class Buff
    {
        protected readonly PlayerEntity Player;
        protected readonly BuffData BuffData;
        private float _timer;

        protected Buff(PlayerEntity player, BuffData buffData)
        {
            Player = player;
            BuffData = buffData;
        }

        public void UpdateTime(float elapsedTime)
        {
            _timer += elapsedTime;
            if (_timer < BuffData.buffTime) return;
            
            DeBuff();
            Player.State.RemoveBuff(this);
        }

        protected abstract void DeBuff();
    }
}