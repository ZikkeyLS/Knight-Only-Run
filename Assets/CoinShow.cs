using System.Collections;
using UnityEngine;

public class CoinShow : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _text;

    private string GetProperEnding(int number)
    {
        if (number % 100 == 1)
            return "�";
        else if (number % 100 == 2 || number % 100 == 3 || number % 100 == 4)
            return "�";
        else
            return "";
    }

    private IEnumerator Start()
    {
        _text.text = $"{GameData.Instance.Values.Coins.GetValue()} �����{GetProperEnding(GameData.Instance.Values.Coins.GetValue())}";
        yield return new WaitUntil(() => YandexInteraction.Instance.IsDataLoaded);
        _text.text = $"{GameData.Instance.Values.Coins.GetValue()} �����{GetProperEnding(GameData.Instance.Values.Coins.GetValue())}";
    }
}
