using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string ANIM_IDLE = "stand";
    private const string ANIM_RUN = "run";

    private const float CROSS_FADE_TIME = .1f;
    private const float ANIM_DEFAULT_SPEED = .5f;
    private const float SNAP_ANGLE = 45f;

    [SerializeField] private Animation _viewAnimation;
    [SerializeField] private Animation _shadowAnimation;

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
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        _angles.y = Mathf.Round((-Mathf.Atan2(y, x) * Mathf.Rad2Deg+90) /SNAP_ANGLE) * SNAP_ANGLE;

        _viewTransform.localEulerAngles = _angles;
        _shadowTransform.localEulerAngles = _angles;
        
        if (Math.Abs(x) > Mathf.Epsilon || Math.Abs(y) > Mathf.Epsilon)
            Run();
        else
            Stop();
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
        _viewAnimation.Play(ANIM_IDLE);
        _shadowAnimation.Play(ANIM_IDLE);
    }
}

