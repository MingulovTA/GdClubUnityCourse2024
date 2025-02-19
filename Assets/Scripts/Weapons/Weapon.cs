using System.Collections;
using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _weaponView;
    [SerializeField] private WpnId _wpnId;
    [SerializeField] private BulletId _bulletId;
    [SerializeField] private BoneId _boneId;
    [SerializeField] private int _damage;
    [SerializeField] private float _delayBeforeNextShot = 1f;
    [SerializeField] private float _delayBeforeSpawnBullet = 0.5f;
    [SerializeField] private string _attackAnimation;

    private Actor _actor;
    private bool _isAttack;
    private float _attackTimer;
    private Bullet _bulletPrefab;
    public WpnId WpnId => _wpnId;
    
    public void Init(Actor actor)
    {
        _actor = actor;
        if (_weaponView != null)
        {
            Bone bone = _actor.ActorAnimator.Bones.FirstOrDefault(b => b.BoneId == _boneId);
            _weaponView.SetParent(bone != null ? bone.Transform : _actor.transform);
        }

        _bulletPrefab = Resources.Load<Bullet>($"Bullets/{_bulletId}");
    }

    public void StartAttack()
    {
        _isAttack = true;
    }

    public void StopAttack()
    {
        _isAttack = false;
    }

    private void OnDisable()
    {
        if (_weaponView!=null)
            Destroy(_weaponView.gameObject);
    }

    private void Update()
    {
        if (_attackTimer > 0)
            _attackTimer -= Time.deltaTime;

        if (_isAttack && _attackTimer <= 0)
            StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        _actor.ActorAnimator.PlayAnimation(_attackAnimation);
        _attackTimer = _delayBeforeNextShot;
        yield return new WaitForSeconds(_delayBeforeSpawnBullet);
        Bullet bullet = Instantiate(_bulletPrefab, _actor.transform);
        bullet.Init(_damage, _actor);
    }
    
    
}