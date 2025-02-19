using ArpaSubmodules.ArpaCommon.General.Extentions.Tween;
using DG.Tweening;
using UnityEngine;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private SpriteRenderer _stamina;
    [SerializeField] private Transform _staminaTransform;
    [SerializeField] private GameObject _separators;

    private Tween _tweenTransform;
    private Tween _tweenColor;
    private void OnEnable()
    {
        _health.OnValueChanged += ValueChangedHandler;
        UpdateView();
    }

    private void OnDisable()=>_health.OnValueChanged -= ValueChangedHandler;

    private void ValueChangedHandler()
    {
        float value = (float)_health.Value / _health.MaxValue;
        _tweenTransform?.Kill();
        _tweenTransform = _staminaTransform.DOScaleX(value, 0.1f);
        _tweenColor?.Kill();
        _tweenColor = _stamina.SetColor(new Color(1f - value, value, 0, 1));
        _separators.SetActive(value > 0);
        _staminaTransform.gameObject.SetActive(value > 0);
    }

    private void UpdateView()
    {
        float value = (float)_health.Value / _health.MaxValue;
        _staminaTransform.localScale = new Vector3(value,1,1);
        _stamina.color = new Color(1f - value, value, 0, 1);
        _separators.SetActive(value > 0);
        _staminaTransform.gameObject.SetActive(value > 0);
    }
}
