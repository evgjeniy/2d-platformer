using Utils;

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

    private void OnApplicationFocus(bool isFocused) => FocusChanged(isFocused);

    private void OnApplicationPause(bool isPaused) => FocusChanged(!isPaused);

    private void FocusChanged(bool isFocused)
    {
        Sound.State = isFocused;
        
        if (First.isPlaying)
        {
            if (isFocused) UnPause();
            else Pause();
        }
        else
        {
            if (isFocused) Play();
            else Stop();
        }
    }
}