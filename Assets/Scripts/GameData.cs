using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameValue<T>
{
    [SerializeField] private T _value;
    private Action _onChanged;

    public GameValue()
    {

    }

    public GameValue(T initial)
    {
        _value = initial;
    }

    public void SetValue(T value, bool callChanged = true) 
    {
        _value = value;

        if (_onChanged != null && _onChanged.GetInvocationList().Length != 0 && callChanged)
            _onChanged.Invoke();
    } 

    public T GetValue() => _value;

    public void SubscribeChanged(Action action) => _onChanged += action;
    public void UnsubscribeChanged(Action action) => _onChanged -= action;
}

[Serializable]
public class GameValues
{
    public Action _onDeserialize;
    public GameValue<int> MaxLevel = new GameValue<int>(0);
    public GameValue<int> Coins = new GameValue<int>(0);

    public void SubscribeOnDeserialize(Action action) => _onDeserialize += action;
    public void UnsubscribeOnDeserialize(Action action) => _onDeserialize -= action;

    public string Serialize() => JsonUtility.ToJson(this);

    public void Deserialize(GameValues values)
    {
        MaxLevel.SetValue(values.MaxLevel.GetValue(), false);
        Coins.SetValue(values.Coins.GetValue(), false);

        if (_onDeserialize != null && _onDeserialize.GetInvocationList().Length != 0)
                _onDeserialize.Invoke();
    }
}

public class LoadedLevel
{
    public readonly int ID;
    public readonly string Name;

    public LoadedLevel(int id, string name)
    {
        ID = id;
        Name = name;
    }
}

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }
    public GameValues Values { get; private set; } = new GameValues();
    public List<LoadedLevel> Levels = new List<LoadedLevel>();
    public int CurrentLevel = 0;

    public bool Online { get; private set; } = false;
    public bool Loaded { get; private set; } = false;
    public void SetOnline(bool online) => Online = online;
    public void SetLoaded(bool loaded) => Loaded = loaded;

    public void UpdateValues(GameValues values) => Values = values;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);
        Instance = this;
    }
}
