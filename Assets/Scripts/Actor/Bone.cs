using UnityEngine;

public class Bone : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private BoneId _boneId;

    public Transform Transform => _transform;
    public BoneId BoneId => _boneId;

    private void OnValidate()
    {
        _transform = transform;
    }
}
