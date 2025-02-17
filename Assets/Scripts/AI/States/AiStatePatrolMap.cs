using System.Collections;
using UnityEngine;

public class AiStatePatrolMap : AiBotStateBase
{
    public override AiStateId AiStateId => AiStateId.PatrolMap;
    public override bool CanTransition => !_aiBot.Sence.IsSeeEnemy; //сенс не видит и не чувствует игрока;

    private AiActorMovement _aiActorMovement;
    private AiBot _aiBot;

    public AiStatePatrolMap(AiBot aiBot)
    {
        _aiActorMovement = aiBot.AiActorMovement;
        _aiBot = aiBot;
    }

    protected override void Enter()
    {
        Vector3 randomPoint = new Vector3(Random.Range(-4f,4f),Random.Range(-2f,2f),0);
        _aiActorMovement.MoveToPoint(randomPoint,OnComplete);
    }

    private void OnComplete()
    {
        _onComplete?.Invoke();
    }

    protected override void Exit()
    {
        _aiActorMovement.StopMoving();
    }
}
