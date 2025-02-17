public class AiStateIdle : AiBotStateBase
{
    public override AiStateId AiStateId => AiStateId.Idle;
    public override bool CanTransition => false;
    protected override void Enter()
    {
        
    }

    protected override void Exit()
    {
        
    }
}