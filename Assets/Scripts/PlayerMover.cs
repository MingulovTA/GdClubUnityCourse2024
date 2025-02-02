using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    private const string AXIS_HORISONTAL = "Horizontal";
    
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _speed = 500f;

    private Vector2 _velocity;

    private void Update()=>_velocity.x = Input.GetAxis(AXIS_HORISONTAL);

    private void FixedUpdate()=>_rigidbody2D.velocity = _velocity*_speed*Time.fixedDeltaTime+Physics2D.gravity;

    private void OnValidate() => _rigidbody2D = GetComponent<Rigidbody2D>();
}
