using Entities.Player;
using InputScripts;
using UnityEngine;
using Utils;

namespace Spawners
{
    public class PlayerSpawner : BaseSpawner<PlayerEntity>
    {
        [SerializeField] private CameraFollow levelCamera;

        public static bool IsTwoPlayers { get; set; }

        protected override void PostAwake()
        {
            CreatePlayer(PlayerInputType.FirstPlayer);
        
            if (IsTwoPlayers) CreatePlayer(PlayerInputType.SecondPlayer);
        
            Destroy(this);
        }
    
        private void CreatePlayer(PlayerInputType inputType)
        {
            var newPlayer = Spawn(position);
            newPlayer.SetupPlayer(inputType);

            var playerSaveKey = inputType.GetSaveKey();
            if (PlayerPrefs.HasKey(playerSaveKey))
                newPlayer.Character.FromJson(PlayerPrefs.GetString(playerSaveKey));

            if (levelCamera != null) levelCamera.AddTarget(newPlayer.transform);
        }
    }
}