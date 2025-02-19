using System;
using System.Collections;
using ArpaSubmodules.ArpaCommon.General.Extentions.Tween;
using DG.Tweening;
using UnityEngine;

public class Bullet: MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spr;
    [SerializeField] private float _tifeTime = 0.1f;
    [SerializeField] private bool _destroyAfterHit;
    private Actor _attacker;
    private int _damage;
    private Tween _tween;
    public void Init(int damage, Actor actorAttacker)
    {
        _attacker = actorAttacker;
        _damage = damage;
    }

    private IEnumerator Start()
    {
        _tween = _spr.SetAlpha(0, _tifeTime).SetEase(Ease.Linear);
        yield return new WaitForSeconds(_tifeTime);
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        _tween?.Kill();
    }

    private void OnTriggerEnter(Collider other)
    {
        Actor target = other.GetComponent<Actor>();
        if (target != null && target.ActorClassId != _attacker.ActorClassId)
        {
            _attacker.Health.TakeDamage(_damage,_attacker);
            if (_destroyAfterHit)
                Destroy(gameObject);
        }
    }
}