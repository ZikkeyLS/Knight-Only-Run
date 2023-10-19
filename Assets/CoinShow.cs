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

    private void Update()
    {
        int coins = GameData.Instance.Values.Coins.GetValue();
        _text.text = $"{coins} �����{GetProperEnding(coins)}";
    }
}
