using UnityEngine;

public class OneWayDoor : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private Vector3 _pos;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        _pos = _transform.position;
        _pos.z = _player.position.z - (_player.position.y > transform.position.y ? 1 : -1) * .1f;
        _transform.position = _pos;
    }
}
