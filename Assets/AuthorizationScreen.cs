using System.Collections;
using UnityEngine;

public class AuthorizationScreen : MonoBehaviour
{
    [SerializeField] private GameObject Transition;

    private void Start()
    {
        if (GameData.Instance.Loaded)
            ChangeScreen();
        else
            StartCoroutine(CheckAuth());
    }

    private IEnumerator CheckAuth()
    {
        yield return new WaitUntil(() => YandexInteraction.Instance.IsInitialized);
        YandexAPI.CheckAuth();
        StartCoroutine(WaitCheckAuth());
    }

    private IEnumerator WaitCheckAuth()
    {
        GameData.Instance.Values.SubscribeOnDeserialize(Loaded);
        yield return new WaitUntil(() => YandexInteraction.Instance.IsAuthorized);
        YandexInteraction.Instance.LoadData();
    }

    public void PlayOnline()
    {
        GameData.Instance.SetOnline(true);
        YandexInteraction.Instance.Authorize();
    }

    public void PlayOffline()
    {
        GameData.Instance.SetOnline(false);
        GameData.Instance.Values.SubscribeOnDeserialize(Loaded);
        YandexInteraction.Instance.LoadData();
    }

    public void Loaded()
    {
        ChangeScreen();
        GameData.Instance.Values.UnsubscribeOnDeserialize(Loaded);
    }

    private void ChangeScreen()
    {
        Debug.Log("Changed screen");
        GameData.Instance.SetLoaded(true);
        Transition.SetActive(true);
        gameObject.SetActive(false);
    }
}
