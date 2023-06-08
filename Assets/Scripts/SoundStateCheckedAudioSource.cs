using UnityEngine;
using Utils;

[RequireComponent(typeof(AudioSource))]
public class SoundStateCheckedAudioSource : MonoCashed<AudioSource>
{
    [SerializeField] private bool playOnAwake;

    private bool FocusedSoundState => Sound.State && Application.isFocused;
    
    protected override void PostAwake()
    {
        if (playOnAwake) Play();
    }

    private void OnApplicationFocus(bool hasFocus) => FocusChanged(hasFocus);

    public void PlayOneShot(AudioClip clip) { if (FocusedSoundState) First.PlayOneShot(clip); }
    
    public void Play() { if (FocusedSoundState) First.Play(); }
    
    public void Stop() { First.Stop(); }
    
    public void UnPause() { if (FocusedSoundState) First.UnPause(); }
    
    public void Pause() { First.Pause(); }

    protected void FocusChanged(bool isFocused)
    {
        if (isFocused) UnPause();
        else Pause();
    }
}