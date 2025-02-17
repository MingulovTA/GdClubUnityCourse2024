using System;
using System.Collections.Generic;
using UnityEngine;

public class AiStatePatrolPath : AiBotStateBase
{
    public override AiStateId AiStateId => AiStateId.PatrolPath;
    public override bool CanTransition => !_aiBot.Sence.IsSeeEnemy; //сенс не видит и не чувствует игрока;

    private AiActorMovement _aiActorMovement;
    private AiBot _aiBot;
    private List<Transform> _path;
    private bool _isLooping;
    private int _currentWpIndex;
    private Action _onCompleteExternal;
    
    public AiStatePatrolPath(AiBot aiBot)
    {
        _aiActorMovement = aiBot.AiActorMovement;
        _aiBot = aiBot;
    }

    public void Init(List<Transform> path, bool isLooping = true, int currentWpIndex = 0,Action onCompleteExternal = null)
    {
        _path = path;
        _isLooping = isLooping;
        _currentWpIndex = currentWpIndex;
        _onCompleteExternal = onCompleteExternal;
    }

    protected override void Enter()
    {
        _aiActorMovement.MoveToPoint(_path[_currentWpIndex].position, OnComplete);
    }

    private void OnComplete()
    {
        if (!_isLooping && _currentWpIndex == _path.Count)
        {
            _currentWpIndex = 0;
            _onComplete?.Invoke();
            _onCompleteExternal?.Invoke();
            return;
        }
        
        _currentWpIndex++;
        if (_currentWpIndex >= _path.Count)
            _currentWpIndex = 0;
        _aiActorMovement.MoveToPoint(_path[_currentWpIndex].position, OnComplete);
    }

    protected override void Exit()
    {
        _aiActorMovement.StopMoving();
    }
}
