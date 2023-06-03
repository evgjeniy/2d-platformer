using UnityEngine;
using Utils;

public class BackgroundAudio : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField, Range(0.0f, 1.0f)] private float volume = 0.1f;
    [SerializeField] private bool loop = true;
    [SerializeField] private bool dontDestroyOnLoad = true;

    public bool SoundState => GetComponent<AudioSource>().isPlaying;

    private void Awake()
    {
        if (FindObjectsOfType<BackgroundAudio>().Length > 1) Destroy(gameObject);
        else Initialize();
    }

    private void OnEnable() => FocusChanged(true);

    private void OnDisable() => FocusChanged(false);

    private void OnApplicationFocus(bool isFocused) => FocusChanged(isFocused);

    private void OnApplicationPause(bool isPaused) => FocusChanged(!isPaused);

    private void FocusChanged(bool isFocused)
    {
        var source = gameObject.GetOrAddComponent<AudioSource>(SetupAudioSource);
        
        if (isFocused) source.UnPause();
        else source.Pause();
    }

    private void Initialize()
    {
        if (dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
        SetupAudioSource(gameObject.GetOrAddComponent<AudioSource>(SetupAudioSource));
    }

    private void SetupAudioSource(AudioSource audioSource)
    {
        audioSource.loop = loop;
        audioSource.volume = volume;
        audioSource.clip ??= audioClip;
        audioSource.clip.IfNotNull(_ => audioSource.Play());
    }
}