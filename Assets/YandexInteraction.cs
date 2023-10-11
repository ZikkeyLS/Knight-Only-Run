using System.Collections;
using UnityEngine;
using YG;

public class YandexInteraction : MonoBehaviour
{
    public static YandexInteraction Instance { get; private set; }

    public bool IsAuthorized => YandexGame.auth;
    public bool IsDataLoaded { get; private set; } = false;

    private const string MainLeaderboard = "CoinsLeaderboard";

    private void OnEnable() => YandexGame.GetDataEvent += DataLoaded;

    private void OnDisable() => YandexGame.GetDataEvent -= DataLoaded;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        YandexGame.Instance.InitializationSDK();
        StartCoroutine(Initialized());
    }

    private void Start()
    {
        GameData.Instance.Values.MaxLevel.SubscribeChanged(SaveData);
        GameData.Instance.Values.Coins.SubscribeChanged(SaveLeaderboard);
        GameData.Instance.Values.Coins.SubscribeChanged(SaveData);
    }

    private IEnumerator Initialized()
    {
        yield return new WaitUntil(() => YandexGame.SDKEnabled);

        YandexGame.Instance._StickyAdActivity(true);
    }

    private void Authorized()
    {
        LoadData();
    }

    private void DataLoaded()
    {
        GameData.Instance.Values.MaxLevel.SetValue(YandexGame.savesData.level);
        GameData.Instance.Values.Coins.SetValue(YandexGame.savesData.money);

        IsDataLoaded = true;
    }

    private void SaveLeaderboard()
    {
        YandexGame.NewLeaderboardScores(MainLeaderboard, GameData.Instance.Values.Coins.GetValue());
    }

    public void Authorize()
    {
        if (YandexGame.SDKEnabled == false)
            return;

        YandexGame.Instance.ResolvedAuthorization.AddListener(Authorized);
        YandexGame.AuthDialog();
    }

    public void SaveData()
    {
        if (IsAuthorized == false)
            return;

        YandexGame.savesData.money = GameData.Instance.Values.Coins.GetValue();
        YandexGame.savesData.level = GameData.Instance.Values.MaxLevel.GetValue();

        YandexGame.SaveProgress();
    }

    public void LoadData()
    {
        YandexGame.LoadProgress();
    }

    public void ShowInterstitial()
    {
        YandexGame.Instance.OpenFullAd();
    }
}
