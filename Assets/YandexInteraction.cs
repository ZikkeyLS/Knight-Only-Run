using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

public static class YandexAPI
{
    [DllImport("__Internal")]
    public static extern void CheckAuth();

    [DllImport("__Internal")]
    public static extern void Auth();

    [DllImport("__Internal")]
    public static extern void RateGame();

    [DllImport("__Internal")]
    public static extern void SaveExtern(string data);

    [DllImport("__Internal")]
    public static extern void LoadExtern();

    [DllImport("__Internal")]
    public static extern void GameReady();

    [DllImport("__Internal")]
    public static extern void ShowStickyAd();

    [DllImport("__Internal")]
    public static extern void HideStickyAd();

    [DllImport("__Internal")]
    public static extern void ShowInterstitialAd();

    [DllImport("__Internal")]
    public static extern void SetLeaderboardScore(string name, int score);
}

public class YandexInteraction : MonoBehaviour
{
    public static YandexInteraction Instance { get; private set; }

    public bool IsAuthorized = false;
    public bool IsInitialized = false;

    private const string MainLeaderboard = "MainLeaderboard";

    public UnityEvent OnInterstitial = new UnityEvent();
    public UnityEvent OnRateGame = new UnityEvent();

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        Time.timeScale = 1;
    }

    public void SetInitialized()
    {
        IsInitialized = true;
    }

    public void SetAuthorized()
    {
        IsAuthorized = true;
    }

    public void SetPlayerInfo(string value)
    {
        GameData.Instance.Values.Deserialize(JsonUtility.FromJson<GameValues>(value));
    }

    public void InterstitialWatched()
    {
        OnInterstitial.Invoke();
    }

    public void RateCompleted()
    {
        OnRateGame.Invoke();
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        StartCoroutine(ShowSticky());
    }

    private IEnumerator ShowSticky()
    {
        yield return new WaitUntil(() => IsInitialized);
        YandexAPI.ShowStickyAd();
    }

    private void Start()
    {
        GameData.Instance.Values.Coins.SubscribeChanged(SaveData);

        OnInterstitial.AddListener(UnPause);
        OnRateGame.AddListener(UnPause);
    }

    private void OnDisable()
    {
        OnInterstitial.RemoveListener(UnPause);
        OnRateGame.RemoveListener(UnPause);
    }

    public void Authorize()
    {
        YandexAPI.Auth();
    }

    public void SaveData()
    {
        if (IsAuthorized)
        {
            YandexAPI.SaveExtern(GameData.Instance.Values.Serialize());
            YandexAPI.SetLeaderboardScore(MainLeaderboard, GameData.Instance.Values.Coins.GetValue());
        }
        else
        {
            PlayerPrefs.SetString("save", GameData.Instance.Values.Serialize());
            PlayerPrefs.Save();
        }
    }

    public void LoadData()
    {
        if (IsAuthorized)
        {
            YandexAPI.LoadExtern();
        }
        else
        {
            GameData.Instance.Values.Deserialize(JsonUtility.FromJson<GameValues>(PlayerPrefs.GetString("save", "{}")));
        }
    }

    public void ShowInterstitial()
    {
        YandexAPI.ShowInterstitialAd();
    }
}
