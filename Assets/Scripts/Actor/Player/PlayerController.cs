using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private const string AXIS_HORISONTAL = "Horizontal";
    private const string AXIS_VERTICAL = "Vertical";
    private const float DASH_COOLDOWN = .2f;
    private const float DASH_SPEED_MULTIPILER = 5f;
    
    [SerializeField] private Actor _actor;
    [SerializeField] private float _speed = 500f;
    [SerializeField] private TrailRenderer _vfxTrail;

    private Vector2 _velocity;
    private bool _isDashing;
    private float _dashColdownTimer;
    private bool CanToDash => !_isDashing && _dashColdownTimer <= 0;

    private void Update()
    {
        _velocity.x = InputService.GetAxis(AXIS_HORISONTAL);
        _velocity.y = InputService.GetAxis(AXIS_VERTICAL);
        
        if (CanToDash && InputService.GetKeyDown(KeyCode.Space))
        {
            _isDashing = true;
            _dashColdownTimer = DASH_COOLDOWN;
            _vfxTrail.enabled = true;
        }

        if (_isDashing)
        {
            if (_dashColdownTimer > 0)
                _dashColdownTimer -= Time.deltaTime;
            else
            {
                _isDashing = false;
                Invoke(nameof(DisableTrail),.2f);
            }
        }
    }

    private void FixedUpdate()
    {
        if (_velocity.magnitude > .5f)
            _velocity = _velocity.normalized;
        
        _actor.Rigidbody2D.velocity =
            _velocity * _speed * Time.fixedDeltaTime * (_isDashing ? DASH_SPEED_MULTIPILER : 1);
    }
    
    private void DisableTrail() => _vfxTrail.enabled = false;
}
