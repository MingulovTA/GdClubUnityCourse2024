using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorAnimator : MonoBehaviour
{
    private const string ANIM_IDLE = "stand";
    private const string ANIM_RUN = "run";
    private const string ANIM_PAIN = "pain";
    private const string ANIM_DIE = "death";

    private const float CROSS_FADE_TIME = .05f;
    private const float ANIM_DEFAULT_SPEED = .5f;
    private const float SNAP_ANGLE = 45f;

    [SerializeField] private Animation _viewAnimation;
    [SerializeField] private Animation _shadowAnimation;
    [SerializeField] private List<Bone> _bones;
    [SerializeField] private Actor _actor;

    private Transform _viewTransform;
    private Transform _shadowTransform;
    private bool _isRunning;
    private Vector3 _angles;
    
    private bool _isAnimationPlaying;
    private Coroutine _animationCoroutine;
    public List<Bone> Bones => _bones;

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
        if (_actor.Health.Value<=0) return;
        float x = _actor.Rigidbody2D.velocity.x;
        float y = _actor.Rigidbody2D.velocity.y;

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
        if (_isRunning||_isAnimationPlaying||_actor.Health.Value<=0) return;
        _isRunning = true;
        _viewAnimation.CrossFade(ANIM_RUN, CROSS_FADE_TIME);
        _shadowAnimation.CrossFade(ANIM_RUN, CROSS_FADE_TIME);
    }

    private void Stop()
    {
        if (!_isRunning||_isAnimationPlaying||_actor.Health.Value<=0) return;
        _isRunning = false;
        _viewAnimation.CrossFade(ANIM_IDLE, CROSS_FADE_TIME);
        _shadowAnimation.CrossFade(ANIM_IDLE, CROSS_FADE_TIME);
    }

    public void PlayAnimation(string animationClipId)
    {
        if (_animationCoroutine!=null)
            StopCoroutine(_animationCoroutine);
        _animationCoroutine = StartCoroutine(Animation(animationClipId));
    }

    private IEnumerator Animation(string animationClipId)
    {
        _isAnimationPlaying = true;
        _viewAnimation[animationClipId].speed = 1f;
        _shadowAnimation[animationClipId].speed = 1f;
        _viewAnimation.CrossFade(animationClipId, CROSS_FADE_TIME);
        _shadowAnimation.CrossFade(animationClipId, CROSS_FADE_TIME);
        yield return new WaitForSeconds(_viewAnimation[animationClipId].clip.length);
        _isAnimationPlaying = false;
        _isRunning = true;
        Stop();
    }

    public void PlayPainAnimation() => PlayAnimation(ANIM_PAIN);

    public void PlayDieAnimation()=> PlayAnimation(ANIM_DIE);
}

