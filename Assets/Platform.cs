using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float _upSeconds = 2f;
    [SerializeField] private float _upSpeed = 2f;
    [SerializeField] private bool _up = true;

    private float _currentSeconds = 0;

    private void Update()
    {
        _currentSeconds += Time.deltaTime;
        if (_currentSeconds > _upSeconds)
        {
            _up = !_up;
            _currentSeconds = 0;
        }
    }

    private void FixedUpdate()
    {
        transform.position += (_up ? 1 : -1) * _upSpeed * Time.fixedDeltaTime * Vector3.up;
    }
}
