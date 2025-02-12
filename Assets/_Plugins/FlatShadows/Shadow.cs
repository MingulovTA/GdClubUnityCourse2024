using System;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    [SerializeField] private Vector2 _sceneSize = new Vector2(4.5f,2.5f);
    [SerializeField] private float _defaultPlayerScale = 2f;
    [SerializeField] private float _scaleShadowMultipiler = 4f;
    [SerializeField] private float _maxShadowAngle = 45f;
    [SerializeField] private Transform _transform;

    private Vector3 _pos;
    private Vector3 _angles;
    private Vector3 _scale;

    private void Awake()
    {
        _scale = Vector3.one * _defaultPlayerScale;
    }

    private void Update()
    {
        _pos = _transform.position;

        _angles.x = _pos.y / _sceneSize.y * _maxShadowAngle;
        _angles.z = -_pos.x / _sceneSize.x * _maxShadowAngle;

        float max = Mathf.Abs(_pos.x) > Mathf.Abs(_pos.y * _scaleShadowMultipiler)
            ? _pos.x
            : _pos.y * _scaleShadowMultipiler;

        _scale.y = Mathf.Abs(max / _sceneSize.x) + Mathf.Abs(max / _sceneSize.y);

        _transform.localScale = _scale;
        _transform.localEulerAngles = _angles;
    }

    private void OnValidate()
    {
        _transform = transform;
    }
}
