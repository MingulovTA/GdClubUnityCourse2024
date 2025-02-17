using UnityEngine;

public class AiChangeState : MonoBehaviour
{
    [SerializeField] private bool _executeOnEnable;
    [SerializeField] private AiBot _aiBot;
    [SerializeField] private AiStateId _aiStateId;
    
    private void Start()
    {
        if (_executeOnEnable)
            Execute();
    }


    public void Execute()
    {
        _aiBot.ChangeState(_aiStateId);
    }
}
