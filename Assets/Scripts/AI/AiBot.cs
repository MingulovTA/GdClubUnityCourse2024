using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AiBot : MonoBehaviour
{
    [SerializeField] private Actor _actor;
    [SerializeField] private AiSence _sence;
    [SerializeField] private AiStateId _aiStateId;
    [SerializeField] private AiActorMovement _aiActorMovement;

    private List<AiBotStateBase> _botStates;
    private AiBotStateBase _aiBotState;
    
    public AiBotStateBase AiBotState => _aiBotState;
    public AiSence Sence => _sence;
    public Actor Actor => _actor;
    public AiActorMovement AiActorMovement => _aiActorMovement;

    private void Awake()
    {
        _botStates = new List<AiBotStateBase>();
        _botStates.Add(new AiStateIdle());
        _botStates.Add(new AiStatePatrolMap(this));
        _botStates.Add(new AiStateAttackEnemy(this));
        _botStates.Add(new AiStatePatrolPath(this));
        _botStates.Add(new AiStateFollow(this));
        ChangeState(_botStates[0]);
    }

    private void OnEnable()
    {
        _sence.OnSeeEnemy += UpdateAi;
        _sence.OnLostEnemy += UpdateAi;
        _actor.Health.OnTakeDamage += TakeDamageHandler;
    }
    
    private void OnDisable()
    {
        _sence.OnSeeEnemy -= UpdateAi;
        _sence.OnLostEnemy -= UpdateAi;
        _actor.Health.OnTakeDamage -= TakeDamageHandler;
    }

    public T GetState<T>(AiStateId aiStateId) where T : AiBotStateBase =>
        _botStates.FirstOrDefault(bs => bs.AiStateId == aiStateId) as T;
    
    public void ChangeState(AiStateId aiStateId)
    {
        _aiBotState?.ExitState();
        _aiBotState = _botStates.FirstOrDefault(bs=>bs.AiStateId==aiStateId);
        _aiBotState.EnterState(UpdateAi); //Yeah, im gonna fail now!
        _aiStateId = _aiBotState.AiStateId;
    }

    private void ChangeState(AiBotStateBase aiState)
    {
        _aiBotState?.ExitState();
        _aiBotState = aiState;
        _aiBotState.EnterState(UpdateAi);
        _aiStateId = _aiBotState.AiStateId;
    }

    private void TakeDamageHandler(Actor attacker)
    {
        if (_aiBotState==null||_aiBotState.AiStateId!=AiStateId.AttackEnemy)
            transform.LookAt(attacker.transform);
        UpdateAi();
    }

    private void UpdateAi()
    {
        foreach (var aiBotStateBase in _botStates)
        {
            if (!aiBotStateBase.CanTransition) continue;
            ChangeState(aiBotStateBase);
            return;
        }
    }
}
