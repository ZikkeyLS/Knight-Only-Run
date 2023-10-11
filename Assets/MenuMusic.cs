using UnityEngine;
using UnityEngine.UI;

public class MenuMusic : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    [SerializeField] private AudioSource _music;

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(ToggleAudio);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(ToggleAudio);
    }

    public void ToggleAudio(bool active)
    {
        if (!active)
            _music.Pause();
        else
            _music.UnPause();
    }
}
