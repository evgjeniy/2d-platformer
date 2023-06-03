using System.Collections;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;

public class YangexAds : MonoBehaviour
{
    [SerializeField, Min(180.0f)] private float timeToShowInterstitial = 180.0f;
    
    [SerializeField, Space] private UnityEvent onRewarded;
    [SerializeField, Space] private UnityEvent<string> onChangeLanguage;

    private void Awake() => YandexGamesSdk.CallbackLogging = true;

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif
        if (YandexGamesSdk.IsInitialized) yield break;

        // Always wait for it if invoking something immediately in the first scene.
        yield return YandexGamesSdk.Initialize();

        while (true)
        {
            // _authorizationStatusText.color = PlayerAccount.IsAuthorized ? Color.green : Color.red;
            // if (PlayerAccount.IsAuthorized)
            //    _personalProfileDataPermissionStatusText.color =
            //        PlayerAccount.HasPersonalProfileDataPermission ? Color.green : Color.red;
            // else
            //     _personalProfileDataPermissionStatusText.color = Color.red;

            yield return new WaitForSecondsRealtime(0.25f);
        }
        
        PlayerAccount.GetProfileData(result =>
        {
            if (result != default) onChangeLanguage?.Invoke(result.lang);
        });

        OnShowInterstitialButtonClick();
        OnShowStickyAdButtonClick();
    }

    private IEnumerator InterstitialCoroutine()
    {
        yield return new WaitForSeconds(timeToShowInterstitial);
        OnShowInterstitialButtonClick();
    }

    public void OnShowInterstitialButtonClick() => InterstitialAd.Show(onCloseCallback: _ => StartCoroutine(InterstitialCoroutine()));

    public void OnShowVideoButtonClick()
    {
        var backgroundAudio = FindObjectOfType<BackgroundAudio>()?.GetComponent<AudioSource>();

        if (backgroundAudio == null || backgroundAudio.isPlaying == false)
            VideoAd.Show(onRewardedCallback: onRewarded.Invoke);
        else
            VideoAd.Show(onRewardedCallback: onRewarded.Invoke,
                onOpenCallback: backgroundAudio.Pause,
                onCloseCallback: backgroundAudio.UnPause,
                onErrorCallback: _ => backgroundAudio.UnPause());
    }

    public void OnShowStickyAdButtonClick() => StickyAd.Show();

    public void OnHideStickyAdButtonClick() => StickyAd.Hide();

    public void OnAuthorizeButtonClick() => PlayerAccount.Authorize();

    public void OnRequestPersonalProfileDataPermissionButtonClick() => PlayerAccount.RequestPersonalProfileDataPermission();

    public void OnGetProfileDataButtonClick()
    {
        PlayerAccount.GetProfileData((result) =>
        {
            var playerName = result.publicName;
            if (string.IsNullOrEmpty(playerName)) playerName = "Anonymous";
            Debug.Log($"My id = {result.uniqueID}, name = {playerName}");
        });
    }

    public void OnSetLeaderboardScoreButtonClick()
    {
        Leaderboard.SetScore("PlaytestBoard", Random.Range(1, 100));
    }

    public void OnGetLeaderboardEntriesButtonClick()
    {
        Leaderboard.GetEntries("PlaytestBoard", result =>
        {
            Debug.Log($"My rank = {result.userRank}");
            foreach (var entry in result.entries)
            {
                var playerName = entry.player.publicName;
                if (string.IsNullOrEmpty(playerName)) playerName = "Anonymous";
                Debug.Log(playerName + " " + entry.score);
            }
        });
    }

    public void OnGetLeaderboardPlayerEntryButtonClick()
    {
        Leaderboard.GetPlayerEntry("PlaytestBoard", (result) =>
        {
            Debug.Log(result == null
                ? "Player is not present in the leaderboard."
                : $"My rank = {result.rank}, score = {result.score}");
        });
    }

    public void OnSetCloudSaveDataButtonClick()
    {
        // PlayerAccount.SetCloudSaveData(_cloudSaveDataInputField.text);
    }

    public void OnGetCloudSaveDataButtonClick()
    {
        // PlayerAccount.GetCloudSaveData(data => _cloudSaveDataInputField.text = data);
    }

    public void OnGetEnvironmentButtonClick()
    {
        Debug.Log($"Environment = {JsonUtility.ToJson(YandexGamesSdk.Environment)}");
    }
}