using Spawners;
using UnityEngine;
using Utils;

public class MainMenu : SceneLoader
{
    [SerializeField] private string firstLevelName = "Level 1";
    
    public void PlayGame(bool isTwoPlayers)
    {
        PlayerSpawner.IsTwoPlayers = isTwoPlayers;

        var sceneName = YandexCloudSaveData.Get(StringConstants.LastLevelSceneSaveKey, firstLevelName);
        Debug.Log(sceneName); 
        LoadSceneNextFrame(sceneName);
    }
}