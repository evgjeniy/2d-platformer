using Entities.Player;
using InputScripts;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Spawners
{
    public class PlayerSpawner : BaseSpawner<PlayerEntity>
    {
        [Header("Dependencies")]
        [SerializeField] private CameraFollow levelCamera;
        [SerializeField] private UnityEvent onGameOver;
        
        public static bool IsTwoPlayers { get; set; }

        protected override void PostAwake()
        {
            CreatePlayer(PlayerInputType.FirstPlayer);

            if (IsTwoPlayers) CreatePlayer(PlayerInputType.SecondPlayer);

            Destroy(gameObject);
        }

        private void CreatePlayer(PlayerInputType inputType)
        {
            var newPlayer = Spawn(position);
            newPlayer.SetupPlayer(inputType);

            TryLoadPlayerJson(inputType, newPlayer);

            levelCamera.IfNotNull(followCamera =>
            {
                followCamera.AddTarget(newPlayer.transform);
                SetupPlayerDeadEvent(newPlayer);
            });

            SetupLevelCompleteOnBossDeadEvent(newPlayer);
        }

        protected virtual void SetupLevelCompleteOnBossDeadEvent(PlayerEntity playerEntity) {}

        private static void TryLoadPlayerJson(PlayerInputType inputType, PlayerEntity newPlayer)
        {
            var playerSaveKey = inputType.GetSaveKey();
            var playerJson = YandexCloudSaveData.Get(playerSaveKey);
            if (playerJson != null)
                newPlayer.Character.FromJson(playerJson);
        }

        private void SetupPlayerDeadEvent(PlayerEntity newPlayer) => newPlayer.State.OnDead += () =>
        {
            foreach (var target in levelCamera.Targets)
                target.GetComponent<PlayerEntity>().IfNotNull(player => player.Disable());

            onGameOver?.Invoke();
        };
    }
}