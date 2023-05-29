using UnityEngine;
using Utils;

public class AudioSourceSetup : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField, Range(0.0f, 1.0f)] private float volume = 0.1f;
    [SerializeField] private bool loop = true;
    [SerializeField] private bool dontDestroyOnLoad = true;

    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("Background Audio").Length > 1) 
            Destroy(gameObject);
        else 
            Initialize();
    }

    private void Initialize()
    {
        if (dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
        SetupAudioSource(gameObject.GetOrAddComponent<AudioSource>(SetupAudioSource));
        Destroy(this);
    }

    private void SetupAudioSource(AudioSource audioSource)
    {
        audioSource.loop = loop;
        audioSource.volume = volume;
        audioSource.clip ??= audioClip;
        audioSource.clip.IfNotNull(_ => audioSource.Play());
    }
}