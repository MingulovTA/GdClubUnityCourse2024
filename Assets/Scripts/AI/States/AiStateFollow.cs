using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateFollow : AiBotStateBase
{
    public override AiStateId AiStateId => AiStateId.FollowTo;
    public override bool CanTransition => !_aiBot.Sence.IsSeeEnemy; //сенс не видит и не чувствует игрока;

    private AiActorMovement _aiActorMovement;
    private AiBot _aiBot;
    private List<Transform> _path;
    private Transform _target;
    
    public AiStateFollow(AiBot aiBot)
    {
        _aiActorMovement = aiBot.AiActorMovement;
        _aiBot = aiBot;
    }

    public void Init(Transform target)
    {
        _target = target;
    }

    protected override void Enter()
    {
        _aiActorMovement.FollowTo(_target,1);
    }

    protected override void Exit()
    {
        _aiActorMovement.StopFollowing();
    }
}
