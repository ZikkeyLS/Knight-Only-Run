using UnityEngine;
using Agava.YandexGames;

public class YandexInteraction : MonoBehaviour
{
    public static YandexInteraction Instance { get; private set; }

    private const int minimalSaveChars = 4;

    public bool IsAuthorized => PlayerAccount.IsAuthorized;
    public bool IsDataLoaded { get; private set; } = false;

    private const string MainLeaderboard = "CoinsLeaderboard";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        YandexGamesSdk.Initialize(Initialized);
    }

    private void Initialized()
    {
        StickyAd.Show();
    }

    private void Authorized()
    {
        LoadData();
    }

    private void DataLoaded(string result)
    {
        if (result.Length > minimalSaveChars)
            GameData.Instance.UpdateValues(JsonUtility.FromJson<GameValues>(result));

        GameData.Instance.Values.MaxLevel.SubscribeChanged(SaveData);

        GameData.Instance.Values.Coins.SubscribeChanged(SaveLeaderboard);
        GameData.Instance.Values.Coins.SubscribeChanged(SaveData);

        IsDataLoaded = true;
    }

    private void SaveLeaderboard()
    {
        Leaderboard.SetScore(MainLeaderboard, GameData.Instance.Values.Coins.GetValue());
    }

    public void Authorize()
    {
        PlayerAccount.Authorize(Authorized);
    }

    public void SaveData()
    {
        if (IsAuthorized == false)
            return;

        PlayerAccount.SetCloudSaveData(GameData.Instance.Values.Serialize());
    }

    public void LoadData()
    {
        PlayerAccount.GetCloudSaveData(DataLoaded);
    }

    public void ShowInterstitial()
    {
        InterstitialAd.Show();
    }
}
