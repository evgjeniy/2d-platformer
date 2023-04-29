using Entities.Player;
using InputScripts;
using Spawners;
using UnityEngine;
using Utils;

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
        var newPlayer = Spawn(transform.position);
        newPlayer.SetupPlayer(inputType);

        if (levelCamera != null) 
            levelCamera.AddTarget(newPlayer.transform);
    }
}