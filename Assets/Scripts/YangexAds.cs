using System.Collections;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;
using Utils;

public class YangexAds : MonoBehaviour
{
    [SerializeField, Min(180.0f)] private float timeToShowInterstitial = 180.0f;
    
    [SerializeField, Space] private UnityEvent onRewarded;

    private void Awake() => YandexGamesSdk.CallbackLogging = true;

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif
        if (!YandexGamesSdk.IsInitialized)
            yield return YandexGamesSdk.Initialize();

        OnShowInterstitialButtonClick();
        OnShowStickyAdButtonClick();
    }

    private IEnumerator InterstitialCoroutine()
    {
        yield return new WaitForSeconds(timeToShowInterstitial);
        OnShowInterstitialButtonClick();
    }

    public void OnShowInterstitialButtonClick()
    {
        if (Sound.State)
        {
			InterstitialAd.Show(
                onOpenCallback: () => Sound.State = false,
                onCloseCallback: _ =>
                {
                    Sound.State = true;
                    StartCoroutine(InterstitialCoroutine());
                },
                onErrorCallback: _ =>
                {
                    Sound.State = true;
                    StartCoroutine(InterstitialCoroutine());
                });
        }
        else
        {
            InterstitialAd.Show(
                onCloseCallback: _ => StartCoroutine(InterstitialCoroutine()),
                onErrorCallback: _ => StartCoroutine(InterstitialCoroutine()));
        }
    }

    public void OnShowVideoButtonClick()
    {   
        if (Sound.State)
        {
			VideoAd.Show(onOpenCallback: () => Sound.State = false,
                onCloseCallback: () => Sound.State = true,
                onErrorCallback: _ => Sound.State = true,
                onRewardedCallback: () =>
                {
                    Sound.State = true;
                    onRewarded.Invoke();
                });
        }
        else
        {
            VideoAd.Show(onRewardedCallback: onRewarded.Invoke);
        }
    }

    public void OnShowStickyAdButtonClick() => StickyAd.Show();
}