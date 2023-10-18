using Platformer;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private Button _nextLevel;
    [SerializeField] private TMPro.TMP_Text _levelName;
    [SerializeField] private TMPro.TMP_Text _coins;

    private string GetProperEnding(int number)
    {
        if (number % 100 == 1)
            return "а";
        else if (number % 100 == 2 || number % 100 == 3 || number % 100 == 4)
            return "ы";
        else
            return "";
    }

    public void Initialize()
    {
        _nextLevel.interactable = GameData.Instance.CurrentLevel != GameData.Instance.Levels.Count;
        _levelName.text = $"Уровень {GameData.Instance.CurrentLevel} пройден!";
        _coins.text = $"+{GameManager.Instance.coinsCounter} монет{GetProperEnding(GameManager.Instance.coinsCounter)}";
    }
}
