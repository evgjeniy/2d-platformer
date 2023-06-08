public class BackgroundAudio : SoundStateCheckedAudioSource
{
    protected override void PostAwake()
    {
        if (FindObjectsOfType<BackgroundAudio>().Length > 1) Destroy(gameObject);
        else DontDestroyOnLoad(gameObject);
        
        base.PostAwake();
    }

    private void OnEnable() => FocusChanged(true);

    private void OnDisable() => FocusChanged(false);
}