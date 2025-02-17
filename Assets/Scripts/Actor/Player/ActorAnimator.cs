using System;
using System.Collections.Generic;
using UnityEngine;

public class ActorAnimator : MonoBehaviour
{
    private const string ANIM_IDLE = "stand";
    private const string ANIM_RUN = "run";

    private const float CROSS_FADE_TIME = .05f;
    private const float ANIM_DEFAULT_SPEED = .5f;
    private const float SNAP_ANGLE = 45f;

    [SerializeField] private Animation _viewAnimation;
    [SerializeField] private Animation _shadowAnimation;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    private Transform _viewTransform;
    private Transform _shadowTransform;
    private bool _isRunning;
    private Vector3 _angles;

    private void Awake()
    {
        _viewTransform = _viewAnimation.transform;
        _shadowTransform = _shadowAnimation.transform;

        foreach (AnimationState ass in _viewAnimation)
            ass.speed = ANIM_DEFAULT_SPEED;
        foreach (AnimationState ass in _shadowAnimation)
            ass.speed = ANIM_DEFAULT_SPEED;
    }

    private void Update()
    {
        float x = _rigidbody2D.velocity.x;
        float y = _rigidbody2D.velocity.y;

        if (Math.Abs(x) > Mathf.Epsilon || Math.Abs(y) > Mathf.Epsilon)
        {
            Run();
            _angles.y = Mathf.Round((-Mathf.Atan2(y, x) * Mathf.Rad2Deg+90) /SNAP_ANGLE) * SNAP_ANGLE;
        }
        else
        {
            Stop();
        }
        _viewTransform.localEulerAngles = _angles;
        _shadowTransform.localEulerAngles = _angles;
    }


    private void Run()
    {
        if (_isRunning) return;
        _isRunning = true;
        _viewAnimation.CrossFade(ANIM_RUN, CROSS_FADE_TIME);
        _shadowAnimation.CrossFade(ANIM_RUN, CROSS_FADE_TIME);
    }

    private void Stop()
    {
        if (!_isRunning) return;
        _isRunning = false;
        _viewAnimation.CrossFade(ANIM_IDLE, CROSS_FADE_TIME);
        _shadowAnimation.CrossFade(ANIM_IDLE, CROSS_FADE_TIME);
    }
}

