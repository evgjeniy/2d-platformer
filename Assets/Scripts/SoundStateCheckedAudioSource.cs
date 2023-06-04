using UnityEngine;
using Utils;

[RequireComponent(typeof(AudioSource))]
public class SoundStateCheckedAudioSource : MonoCashed<AudioSource>
{
    [SerializeField] private bool playOnAwake;

    protected override void PostAwake()
    {
        if (playOnAwake) Play();
    }

    public void PlayOneShot(AudioClip clip) { if (Sound.State) First.PlayOneShot(clip); }
    
    public void Play() { if (Sound.State) First.Play(); }
    
    public void Stop() { First.Stop(); }
    
    public void UnPause() { if (Sound.State) First.UnPause(); }
    
    public void Pause() { First.Pause(); }
}