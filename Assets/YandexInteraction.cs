using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public static class YandexAPI
{
    [DllImport("__Internal")]
    public static extern void Init();

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

    public bool IsInitialized = false;
    public bool IsAuthorized = false;

    private const string MainLeaderboard = "CoinsLeaderboard";

    public void SetPlayerInfo(string value)
    {
        GameData.Instance.Values.Deserialize(JsonUtility.FromJson<GameValues>(value));
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        YandexAPI.Init();
        Instance = this;
        StartCoroutine(Initialized());    
    }

    private void Start()
    {
        GameData.Instance.Values.MaxLevel.SubscribeChanged(SaveData);
        GameData.Instance.Values.Coins.SubscribeChanged(SaveData);
    }

    private IEnumerator Initialized()
    {
        yield return new WaitForSeconds(1);
        YandexAPI.GameReady();
        YandexAPI.ShowStickyAd();
    }

    public void Authorize()
    {
        YandexAPI.Auth();
        StartCoroutine(Authorized());
    }

    private IEnumerator Authorized()
    {
        yield return new WaitForSeconds(1f);
        IsAuthorized = true;
        LoadData();
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
