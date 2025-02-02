using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    private const string AXIS_HORISONTAL = "Horizontal";
    private const string AXIS_VERTICAL = "Vertical";
    
    [SerializeField] private Rigidbody2D _rigidbody2D;

    private Vector2 _velocity;

    private void Update()
    {
        _velocity.x = Input.GetAxis(AXIS_HORISONTAL);
        _velocity.y = Input.GetAxis(AXIS_VERTICAL);
        _rigidbody2D.velocity = _velocity;
    }

    private void OnValidate() => _rigidbody2D = GetComponent<Rigidbody2D>();
}
