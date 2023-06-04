using UnityEngine;
using UnityEngine.UI;
using Utils;

[RequireComponent(typeof(Button))]
public class SoundSwitcher : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;
    
    private void Start() => this.InvokeNextFrame(switcher => switcher.UpdateSound());

    public void Switch()
    {
        Sound.State = !Sound.State;
        UpdateSound();
    }

    private void UpdateSound()
    {
        var soundState = Sound.State;
        image.sprite = soundState ? soundOnSprite : soundOffSprite;
        
        var backgroundAudio = FindObjectOfType<BackgroundAudio>();
        if (backgroundAudio == null) return;
        
        if (soundState) backgroundAudio.Enable();
        else backgroundAudio.Disable();
    }
}