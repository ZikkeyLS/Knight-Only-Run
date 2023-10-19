using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private LevelUI _levelInstance;
    [SerializeField] private string[] _levels;

    private void Start()
    {
        Debug.Log("loading levels");
        for (int i = 0; i < _levels.Length; i++)
        {
            LevelUI levelUI = Instantiate(_levelInstance, transform);
            levelUI.Initialize(i, _levels[i], GameData.Instance.Values.MaxLevel.GetValue() >= i);

            if (GameData.Instance.Levels.Count < _levels.Length)
                GameData.Instance.Levels.Add(new LoadedLevel(i, _levels[i]));
        }
    }
}
