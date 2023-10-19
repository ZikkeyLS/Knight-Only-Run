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

        GameData.Instance.Values.SubscribeOnDeserialize(Loaded);
    }

    public void PlayOffline()
    {
        GameData.Instance.SetOnline(false);
        YandexInteraction.Instance.LoadData();
        GameData.Instance.Values.SubscribeOnDeserialize(Loaded);
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
