using UnityEngine;
using UnityEngine.Events;

public class MultiManager : MonoBehaviour
{
    [SerializeField] private UnityEvent _output;
    [SerializeField] private bool _executeOnStart;

    private void Start()
    {
        if (_executeOnStart)
            Execute();
    }

    public void Execute() => _output?.Invoke();
}
