using UnityEngine;

namespace Arpa_common.General.Extentions.Tween
{
    public class RotatingItem : MonoBehaviour
    {
        [SerializeField] private float _speed = 5;
        private Transform _transform;
        private Vector3 _angle;

        private void Awake()
        {
            _transform = transform;
            _angle = _transform.localEulerAngles;
        }


        private void Update()
        {
            _angle+=Vector3.forward*_speed*Time.deltaTime;
            _transform.localEulerAngles = _angle;
        }
    }
}
