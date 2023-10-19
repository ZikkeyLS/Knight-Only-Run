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
        int coins = GameData.Instance.Values.Coins.GetValue();
        _text.text = $"{coins} монет{GetProperEnding(coins)}";
    }
}
