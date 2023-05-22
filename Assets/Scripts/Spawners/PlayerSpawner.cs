using Entities.Player;
using InputScripts;
using UnityEngine;
using Utils;

namespace Spawners
{
    public class PlayerSpawner : BaseSpawner<PlayerEntity>
    {
        [Header("Dependencies")]
        [SerializeField] private CameraFollow levelCamera;
        [SerializeField] private GameObject gameOverUi;

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

            if (levelCamera != null) levelCamera.AddTarget(newPlayer.transform);
            if (gameOverUi != null) SetupPlayerDeadEvent(newPlayer);

            SetupLevelCompleteOnBossDeadEvent(newPlayer);
        }

        protected virtual void SetupLevelCompleteOnBossDeadEvent(PlayerEntity playerEntity) {}

        private static void TryLoadPlayerJson(PlayerInputType inputType, PlayerEntity newPlayer)
        {
            var playerSaveKey = inputType.GetSaveKey();
            if (PlayerPrefs.HasKey(playerSaveKey))
                newPlayer.Character.FromJson(PlayerPrefs.GetString(playerSaveKey));
        }

        private void SetupPlayerDeadEvent(PlayerEntity newPlayer) => newPlayer.State.OnDead += () =>
        {
            foreach (var cameraTarget in levelCamera.Targets)
                if (cameraTarget.TryGetComponent<PlayerEntity>(out var player))
                    player.enabled = false;

            gameOverUi.SetActive(true);
        };
    }
}