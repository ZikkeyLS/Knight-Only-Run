using UnityEngine;

public class CoinShow : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _text;

    private string GetProperEnding(int number)
    {
        if (number % 100 == 1)
            return "а";
        else if (number % 100 == 2 || number % 100 == 3 || number % 100 == 4)
            return "ы";
        else
            return "";
    }

    private void Update()
    {
        _text.text = $"{GameData.Instance.Values.Coins.GetValue()} монет{GetProperEnding(GameData.Instance.Values.Coins.GetValue())}";
    }
}
