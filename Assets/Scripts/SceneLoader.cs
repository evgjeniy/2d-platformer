using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    protected const string LAST_LEVEL_SCENE_SAVE_KEY = nameof(LAST_LEVEL_SCENE_SAVE_KEY);
    
    public void LoadSceneNextFrame(string sceneName) => LoadSceneNextFrame(sceneName, null);

    public void LoadSceneNextFrame(string sceneName, float? delay) => 
        StartCoroutine(LoadSceneCoroutine(sceneName, delay));
    
    private IEnumerator LoadSceneCoroutine(string sceneName, float? delay)
    {
        Time.timeScale = 1.0f;
        DOTween.KillAll();
        
        yield return delay == null ? null : new WaitForSeconds(delay.Value);
        
        LoadScene(sceneName);
    }
    
    public void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);

    public void SaveScene(string sceneName)
    {
        PlayerPrefs.SetString(LAST_LEVEL_SCENE_SAVE_KEY, sceneName);
        PlayerPrefs.Save();
    }
    
#if UNITY_EDITOR

    [ContextMenu("Clear Saved Scene")]
    public void ClearSavedScene()
    {
        PlayerPrefs.DeleteKey(LAST_LEVEL_SCENE_SAVE_KEY);
        PlayerPrefs.Save();
    }
    
#endif
}