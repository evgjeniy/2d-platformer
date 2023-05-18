using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);

    public void LoadSceneNextFrame(string sceneName, float? delay = null) => 
        StartCoroutine(LoadSceneCoroutine(sceneName, delay));
    
    private IEnumerator LoadSceneCoroutine(string sceneName, float? delay)
    {
        yield return delay == null ? null : new WaitForSeconds(delay.Value);

        LoadScene(sceneName);
    }
}