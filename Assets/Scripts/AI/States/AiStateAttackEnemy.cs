using System.Collections;
using UnityEngine;

public class AiStateAttackEnemy : AiBotStateBase
{
    public override AiStateId AiStateId => AiStateId.AttackEnemy;
    public override bool CanTransition => _aiBot.Sence.IsSeeEnemy;
    
    private AiActorMovement _aiActorMovement;
    private AiBot _aiBot;
    private CoroutineRunner _coroutineRunner;
    //private Coroutine _attackCoroutine;
    private Coroutine _moveCoroutine;
    //private Coroutine _aimCoroutine;
    
    public AiStateAttackEnemy(AiBot aiBot)
    {
        _aiActorMovement = aiBot.AiActorMovement;
        _aiBot = aiBot;
        //_coroutineRunner = DiContainer.Instance.Resolve<ICoroutineRunner>();
        _coroutineRunner = CoroutineRunner.Instance;
    }

    protected override void Enter()
    {
        //_attackCoroutine = _coroutineRunner.Run(AttackYield());
        _moveCoroutine = _coroutineRunner.Run(MovingYield());
        //_aimCoroutine = _coroutineRunner.Run(AimYield());
    }

    private IEnumerator MovingYield()
    {
        yield return new WaitForSeconds(Random.Range(0.1f,0.5f));
        //_aiBot.Actor.Mdl.LegsAnim.CrossFade("legs_run",0.1f);
        Vector3 offset = new Vector3(Random.Range(-2f,2f),0,0);
        Vector3 target = _aiBot.transform.position + _aiBot.transform.TransformDirection(offset);
        _aiActorMovement.MoveToPoint(target, _onComplete);
    }
   
    /*private IEnumerator AimYield()
    {
        while (true)
        {
            yield return null;
            _aiBot.Actor.transform.LookAt(_aiBot.Sence.DetectedDetectedActors[0].transform);
            var a = _aiBot.Actor.transform.eulerAngles;
            _aiBot.Actor.transform.eulerAngles = new Vector3(0,a.y,0);
            _aiBot.Actor.ActorWeapons.FireWp.LookAt(_aiBot.Sence.DetectedDetectedActors[0].transform.position+Vector3.up*1.5f);
        }
    }*/
    
    /*private IEnumerator AttackYield()
    {
        while (true)
        {
            _aiBot.Actor.ActorWeapons.StartAttack();
            yield return new WaitForSeconds(Random.Range(0.1f,0.5f));
            _aiBot.Actor.ActorWeapons.StopAttack();
            yield return new WaitForSeconds(Random.Range(0.1f,0.5f));
        }
    }*/

    protected override void Exit()
    {
        //_aiBot.Actor.Mdl.LegsAnim.CrossFade("legs_idle",0.1f);
        //_coroutineRunner.Stop(_attackCoroutine);
        _coroutineRunner.Stop(_moveCoroutine);
        //_coroutineRunner.Stop(_aimCoroutine);
        //_aiBot.Actor.ActorWeapons.StopAttack();
    }
}
