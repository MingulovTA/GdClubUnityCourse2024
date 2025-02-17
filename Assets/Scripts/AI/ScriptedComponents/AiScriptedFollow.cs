using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiScriptedFollow : MonoBehaviour
{
    [SerializeField] private Transform _followTarget;
    [SerializeField] private bool _executeOnEnable;
    [SerializeField] private AiBot _aiBot;
    
    private void Start()
    {
        if (_executeOnEnable)
            Execute();
    }


    public void Execute()
    {
        _aiBot.GetState<AiStateFollow>(AiStateId.FollowTo).Init(_followTarget);
        _aiBot.ChangeState(AiStateId.FollowTo);
    }
}
