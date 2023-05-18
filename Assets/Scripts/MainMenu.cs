using Spawners;
using UnityEngine;

public class MainMenu : SceneLoader
{
    [SerializeField] private string firstLevelName = "Level 1";
    
    public void PlayGame(bool isTwoPlayers)
    {
        PlayerSpawner.IsTwoPlayers = isTwoPlayers;
        LoadScene(firstLevelName);
    }
}