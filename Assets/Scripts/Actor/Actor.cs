using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private ActorClassId _actorClassId;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _moveSpeed = 250;
    [SerializeField] private Health _health;

    public ActorClassId ActorClassId => _actorClassId;
    public Rigidbody2D Rigidbody2D => _rigidbody2D;

    public float MoveSpeed => _moveSpeed;
    public IHealthable Health => _health;
}
