using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Arpa_common.General.Extentions.Tween
{
    public class TweenButton : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField] private float _pressedLocalScaleFactor = 0.9f;
        [SerializeField] private Transform _tweenItem;
        [Header("Optionally")][SerializeField] private Button _button;

        private void Awake()
        {
            if (_tweenItem == null)
                _tweenItem = transform;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_button==null||_button.interactable)
                _tweenItem.localScale = Vector3.one;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_button==null||_button.interactable)
                _tweenItem.localScale = Vector3.one;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_button==null||_button.interactable)
                _tweenItem.localScale = Vector3.one;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_button==null||_button.interactable)
                _tweenItem.localScale = Vector3.one * _pressedLocalScaleFactor;
        }
    }
}
