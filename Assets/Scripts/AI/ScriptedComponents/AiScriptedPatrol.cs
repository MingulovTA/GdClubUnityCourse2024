using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AiScriptedPatrol : MonoBehaviour
{
    [SerializeField] private List<Transform> _patrolPath;
    [SerializeField] private bool _executeOnEnable;
    [SerializeField] private AiBot _aiBot;
    [SerializeField] private bool _isLooping = true;
    [SerializeField] private UnityEvent _onComplete;
    
    private void Start()
    {
        if (_executeOnEnable)
            Execute();
    }


    public void Execute()
    {
        _aiBot.GetState<AiStatePatrolPath>(AiStateId.PatrolPath).Init(_patrolPath, _isLooping, 0, OnComplete);
        _aiBot.ChangeState(AiStateId.PatrolPath);
    }

    private void OnComplete() => _onComplete?.Invoke();
}
