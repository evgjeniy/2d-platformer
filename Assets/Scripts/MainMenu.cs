using Spawners;
using UnityEngine;
using Utils;

public class MainMenu : SceneLoader
{
    [SerializeField] private string firstLevelName = "Level 1";
    
    public void PlayGame(bool isTwoPlayers)
    {
        PlayerSpawner.IsTwoPlayers = isTwoPlayers;

        LoadSceneNextFrame(PlayerPrefs.HasKey(StringConstants.LastLevelSceneSaveKey)
            ? PlayerPrefs.GetString(StringConstants.LastLevelSceneSaveKey)
            : firstLevelName);
    }
}