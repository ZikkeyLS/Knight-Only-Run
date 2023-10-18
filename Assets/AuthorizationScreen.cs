using System.Collections;
using UnityEngine;

public class AuthorizationScreen : MonoBehaviour
{
    [SerializeField] private GameObject Transition;

    private void Start()
    {
        if (GameData.Instance.Loaded)
            ChangeScreen();
    }

    public void PlayOnline()
    {
        GameData.Instance.SetOnline(true);
        YandexInteraction.Instance.Authorize();
        StartCoroutine(WaitUntilLoaded());
    }

    public IEnumerator WaitUntilLoaded()
    {
        yield return new WaitUntil(() => YandexInteraction.Instance.IsDataLoaded);
        GameData.Instance.SetLoaded(true);
        ChangeScreen();
    }

    public void PlayOffline()
    {
        GameData.Instance.SetOnline(false);
        GameData.Instance.SetLoaded(true);
        YandexInteraction.Instance.LoadData();
      //  StartCoroutine(WaitUntilLoadedLocal());
    }

  //  public IEnumerator WaitUntilLoadedLocal()
  //  {
      //  yield return new WaitUntil(() => YandexGamesSdk.IsInitialized);
      //  GameData.Instance.SetLoaded(true);
      //  ChangeScreen();
//}

    private void ChangeScreen()
    {
        Transition.SetActive(true);
        gameObject.SetActive(false);
    }
}
