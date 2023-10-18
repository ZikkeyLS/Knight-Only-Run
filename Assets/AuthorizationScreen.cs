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
        yield return new WaitForSeconds(1f);
        GameData.Instance.SetLoaded(true);
        ChangeScreen();
    }

    public void PlayOffline()
    {
        GameData.Instance.SetOnline(false);
        YandexInteraction.Instance.LoadData();
        StartCoroutine(WaitUntilLoaded());
    }

    private void ChangeScreen()
    {
        Transition.SetActive(true);
        gameObject.SetActive(false);
    }
}
