using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _text;
    [SerializeField] private Button _button;

    private int _id;

    public void Initialize(int id, string levelName, bool unlocked = true)
    {
        _id = id;
        _text.text = (id + 1).ToString();
        _button.interactable = unlocked;
        _button.onClick.AddListener(() => SetCurrentData(levelName));
    }

    public void SetCurrentData(string levelName)
    {
        GameData.Instance.CurrentLevel = _id;
        SceneManager.LoadScene(levelName);
    }
}
