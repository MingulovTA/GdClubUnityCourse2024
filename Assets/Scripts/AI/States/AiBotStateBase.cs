using System;

public abstract class AiBotStateBase
{
    protected Action _onComplete;
    public abstract AiStateId AiStateId { get; }
    
    public abstract bool CanTransition { get; }

    public void EnterState(Action onComplete)
    {
        _onComplete = onComplete;
        Enter();
    }

    protected abstract void Enter();
    
    public void ExitState()
    {
        _onComplete = null;
        Exit();
    }
    
    protected abstract void Exit();
}