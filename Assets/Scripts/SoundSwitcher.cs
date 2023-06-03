using UnityEngine;
using UnityEngine.UI;
using Utils;

[RequireComponent(typeof(Button))]
public class SoundSwitcher : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;

    private bool _soundState;

    private void Start()
    {
        _soundState = PlayerPrefs.GetString(StringConstants.SoundStateKey, "True") == "True";
        this.InvokeNextFrame(switcher => switcher.ChangeSound());
    }
    
    public void Switch()
    {
        _soundState = !_soundState;
        ChangeSound();
    }

    private void ChangeSound()
    {
        image.sprite = _soundState ? soundOnSprite : soundOffSprite;
        
        var backgroundAudio = FindObjectOfType<BackgroundAudio>();
        if (backgroundAudio == null) return;
        
        if (_soundState) backgroundAudio.Enable();
        else backgroundAudio.Disable();
        
        PlayerPrefs.SetString(StringConstants.SoundStateKey, _soundState.ToString());
        PlayerPrefs.Save();
    }
}