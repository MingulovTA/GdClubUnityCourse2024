using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    private const string AXIS_HORISONTAL = "Horizontal";
    private const string AXIS_VERTICAL = "Vertical";
    private const float DASH_COOLDOWN = .2f;
    private const float DASH_SPEED_MULTIPILER = 5f;
    
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _speed = 500f;

    private Vector2 _velocity;
    private bool _isDashing;
    private float _dashColdownTimer;
    private bool CanToDash => !_isDashing && _dashColdownTimer <= 0;
    
    
    private void Update()
    {
        _velocity.x = Input.GetAxis(AXIS_HORISONTAL);
        _velocity.y = Input.GetAxis(AXIS_VERTICAL);

        if (CanToDash && Input.GetKeyDown(KeyCode.Space))
        {
            _isDashing = true;
            _dashColdownTimer = DASH_COOLDOWN;
        }

        if (_isDashing)
        {
            if (_dashColdownTimer > 0)
                _dashColdownTimer -= Time.deltaTime;
            else
                _isDashing = false;
        }
    }

    private void FixedUpdate() => _rigidbody2D.velocity =
        _velocity * _speed * Time.fixedDeltaTime * (_isDashing ? DASH_SPEED_MULTIPILER : 1);


    private void OnValidate() => _rigidbody2D = GetComponent<Rigidbody2D>();
}
