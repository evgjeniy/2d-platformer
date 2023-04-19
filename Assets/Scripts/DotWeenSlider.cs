using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Slider))]
public class DotWeenSlider : MonoCashed<UnityEngine.UI.Slider>
{
    [SerializeField] public float duration = 0.5f;
    [SerializeField] public Ease ease;

    private TweenerCore<float, float, FloatOptions> _currentTween;

    public float TweenSliderValue
    {
        set
        {
            _currentTween?.Kill();
            _currentTween = First.DOValue(value, duration).SetEase(ease).SetLink(gameObject)
                .OnKill(() => { _currentTween = null; First.value = value; }).Play();
        }
    }
}