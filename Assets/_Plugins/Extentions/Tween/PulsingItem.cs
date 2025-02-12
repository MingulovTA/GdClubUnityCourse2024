using DG.Tweening;
using UnityEngine;

namespace Arpa_common.General.Extentions.Tween
{
    public class PulsingItem : MonoBehaviour
    {
        [SerializeField] private Vector3 _startScale = Vector3.one;
        [SerializeField] private Vector3 _targetScale= Vector3.one*1.2f;
        [SerializeField] private float _time = 0.75f;
        [SerializeField] private bool _useUnscaledTime = false;
        private DG.Tweening.Tween _pulsingTween;
        private void OnEnable()
        {
            _pulsingTween?.Kill();
            transform.localScale = _startScale;
            _pulsingTween = transform.DOScale(_targetScale, _time).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
            if (_useUnscaledTime)
                _pulsingTween.SetUpdate(true);
        }

        private void OnDisable()
        {
            _pulsingTween?.Kill();
        }
    }
}
