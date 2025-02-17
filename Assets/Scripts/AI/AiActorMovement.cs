using System;
using System.Collections;
using Arpa_common.General.Extentions;
using UnityEngine;

public class AiActorMovement : MonoBehaviour
{
    private const float CLOSE_DIST = .5f;

    [SerializeField] private Transform _transform;
    [SerializeField] private Actor _actor;
    
    private Vector2 _velocity;
    private Coroutine _following;
    private Coroutine _moving;
    private float _stopDistance;

    public event Action OnStopFollowing;

    private bool IsCloseTo(Transform t) => Vector2.Distance(t.position, _transform.position) < _stopDistance;
    private bool IsCloseTo(Vector2 p) => Vector2.Distance(p, _transform.position) < CLOSE_DIST;

    public void MoveToPoint(Vector2 targetPoint, Action onComplete)
    {
        StopMoving();
        StopFollowing();
        _moving = StartCoroutine(Moving(targetPoint, onComplete));
    }

    public void StopMoving()
    {
        if (_moving != null)
            StopCoroutine(_moving);
    }
    
    public void FollowTo(Transform target, float stopDistance)
    {
        StopMoving();
        StopFollowing();
        _stopDistance = stopDistance;
        _following = StartCoroutine(Following(target));
    }

    public void StopFollowing()
    {
        if (_following != null)
            StopCoroutine(_following);
        OnStopFollowing?.Invoke();
    }

    private IEnumerator Moving(Vector3 targetPoint, Action onComplete)
    {
        targetPoint.z = _transform.position.z;
        yield return new WaitForFixedUpdate();
        while (!IsCloseTo(targetPoint))
        {
            _velocity = targetPoint - _transform.position;
            if (_velocity.magnitude > .5f)
                _velocity = _velocity.normalized;
            _actor.Rigidbody2D.velocity = _velocity * _actor.MoveSpeed * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        onComplete?.Invoke();
    }
    
    private IEnumerator Following(Transform target)
    {
        while (true)
        {
            if (!IsCloseTo(target))
                _velocity = target.position - _transform.position;
            else
                _velocity = _velocity * 0;
            
            if (_velocity.magnitude > .5f)
                _velocity = _velocity.normalized;
            _actor.Rigidbody2D.velocity = _velocity * _actor.MoveSpeed * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
}
