using System.Collections;
using Agava.YandexGames;
using UnityEngine;
namespace Utils
{
    public class YandexSdkInitializeComponent : MonoBehaviour
    {
        private void Awake() => StartCoroutine(Initialize());

        private IEnumerator Initialize()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            Destroy(this);
            yield break;
#endif
            if (!YandexGamesSdk.IsInitialized)
                yield return YandexGamesSdk.Initialize();
            
            Destroy(gameObject);
        }

#if UNITY_EDITOR
        [ContextMenu("Clear Save Dictionary")]
        public void ClearSaveDictionary() => YandexCloudSaveData.DeleteAll();
#endif
    }
}