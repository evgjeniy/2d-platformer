using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class SceneLoader : MonoBehaviour
{
    [SerializeField, Min(0.0f)] private float loadNextLevelDelay = 0.6f;

    public void LoadNextLevel(string sceneName) => LoadSceneNextFrame(sceneName, loadNextLevelDelay);
    
    public void LoadSceneNextFrame(string sceneName) => LoadSceneNextFrame(sceneName, null);
    
    private void LoadSceneNextFrame(string sceneName, float? delay)
    {
        Time.timeScale = 1.0f;
        DOTween.KillAll();
        this.InvokeNextFrame(_ => SceneManager.LoadScene(sceneName), delay);
    }

    public void SaveScene(string sceneName)
    {
        PlayerPrefs.SetString(StringConstants.LastLevelSceneSaveKey, sceneName);
        PlayerPrefs.Save();
    }
    
#if UNITY_EDITOR

    [ContextMenu("Clear Saved Scene")]
    public void ClearSavedScene()
    {
        PlayerPrefs.DeleteKey(StringConstants.LastLevelSceneSaveKey);
        PlayerPrefs.Save();
    }
    
#endif
}