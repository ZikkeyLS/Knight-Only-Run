using System.Collections;
using UnityEngine;
//using Agava.YandexGames;

public class YandexInteraction : MonoBehaviour
{
    public static YandexInteraction Instance { get; private set; }

  //  public bool IsAuthorized => PlayerAccount.IsAuthorized;
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

        //YandexGamesSdk.Initialize(() => Debug.Log("Initialized"));
        //StartCoroutine(Initialized());
    }

    private void Start()
    {
        GameData.Instance.Values.MaxLevel.SubscribeChanged(SaveData);
        GameData.Instance.Values.Coins.SubscribeChanged(SaveData);
    }

   // private IEnumerator Initialized()
   // {
   //     yield return new WaitUntil(() => YandexGamesSdk.IsInitialized);
   //     YandexGamesSdk.GameReady();
   //     StickyAd.Show();
   // }

    private void Authorized()
    {
        LoadData();
    }

    public void Authorize()
    {
       // if (YandexGamesSdk.IsInitialized == false)
       //     return;

       // PlayerAccount.StartAuthorizationPolling(0, Authorized);
    }

    public void SaveData()
    {
       // PlayerAccount.SetCloudSaveData(GameData.Instance.Values.Serialize());
       // Leaderboard.SetScore(MainLeaderboard, GameData.Instance.Values.Coins.GetValue());
    }

    public void LoadData()
    {
        // PlayerAccount.GetCloudSaveData((result) => GameData.Instance.Values.Deserialize(JsonUtility.FromJson<GameValues>(result)));
    }

    public void ShowInterstitial()
    {
        // InterstitialAd.Show();
    }
}
