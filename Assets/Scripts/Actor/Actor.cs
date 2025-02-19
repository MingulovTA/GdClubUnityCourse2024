using System;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private ActorClassId _actorClassId;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _moveSpeed = 250;
    [SerializeField] private Health _health;
    [SerializeField] private ActorAnimator _actorAnimator;

    public ActorClassId ActorClassId => _actorClassId;
    public Rigidbody2D Rigidbody2D => _rigidbody2D;

    public float MoveSpeed => _moveSpeed;
    public IHealthable Health => _health;

    public ActorAnimator ActorAnimator => _actorAnimator;
    public WpnId SelectedWpnId => _wpnId;
    public Weapon SelectedWpn => _weapon;

    private WpnId _wpnId;
    private Weapon _weapon;

    private void Start()
    {
        SelectWeapon(WpnId.Sword);
    }

    private void OnEnable()
    {
        _health.OnTakeDamage += TakeDamageHandler;
    }

    private void OnDisable()
    {
        _health.OnTakeDamage -= TakeDamageHandler;
    }

    public void SelectWeapon(WpnId wpnId)
    {
        if (_weapon != null)
        {
            if (_weapon.WpnId==wpnId) return;
            _weapon.StopAttack();
            Destroy(_weapon.gameObject);
            _weapon = null;
        }

        if (wpnId != WpnId.None)
        {
            if (_wpnId==wpnId) return;
            _wpnId = wpnId;
            _weapon = Resources.Load<Weapon>($"Weapons/{wpnId}");
            _weapon = Instantiate(_weapon, transform);
            _weapon.Init(this);
        }
    }
    
    private void TakeDamageHandler(Actor obj)
    {
        if (_health.Value > 0)
        {
            _actorAnimator.PlayPainAnimation();
        }
        else
        {
            _actorAnimator.PlayDieAnimation();
            _rigidbody2D.isKinematic = true;
            _rigidbody2D.simulated = false;
        }
    }
}
