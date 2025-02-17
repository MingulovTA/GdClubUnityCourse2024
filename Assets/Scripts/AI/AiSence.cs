using System;
using System.Collections.Generic;
using Arpa_common.General.Extentions;
using UnityEngine;

public class AiSence : MonoBehaviour
{
    [SerializeField] private Transform _eyesPos;
    [SerializeField] private LayerMask _layerMask;
    
    private bool _isSuspectEnemy;
    public event Action OnSeeEnemy;
    public event Action OnLostEnemy;

    public bool IsSeeEnemy => _detectedActors.Count > 0;
    public bool IsSuspectEnemy => _isSuspectEnemy;

    [SerializeField] private List<Actor> _detectedActors = new List<Actor>();
    [SerializeField] private List<Actor> _innerActors = new List<Actor>();
    private List<Actor> _detectedActorsForRemove = new List<Actor>();

    public List<Actor> DetectedDetectedActors => _detectedActors;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Actor actor = other.GetComponent<Actor>();
        if (actor != null && !actor.IsOneOf(_innerActors))
            actor.AddTo(_innerActors);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        Actor actor = other.GetComponent<Actor>();
        if (actor != null && actor.IsOneOf(_innerActors))
            _innerActors.Remove(actor);
    }
    

    private void Update()
    {
        foreach (var innerActor in _innerActors)
        {
            RaycastHit hit;

            Vector3 targetDir = innerActor.transform.position + Vector3.up - (_eyesPos.position) ;
            Debug.DrawRay(_eyesPos.position , targetDir);

            if (Physics.Raycast(_eyesPos.position, targetDir, out hit, 1000,_layerMask))
            {
                
                if (hit.transform.GetComponent<Actor>() == innerActor)
                {
                    Debug.DrawLine(_eyesPos.position , hit.point,Color.red);
                    if (!innerActor.IsOneOf(_detectedActors))
                    {
                        innerActor.AddTo(_detectedActors);
                        OnSeeEnemy?.Invoke();
                    }
                }
                else
                {
                    Debug.DrawLine(_eyesPos.position , hit.point,Color.blue);
                    if (innerActor.IsOneOf(_detectedActors))
                    {
                        _detectedActors.Remove(innerActor);
                        OnLostEnemy?.Invoke();
                    }
                }
            }
            else
            {
                Debug.DrawLine(_eyesPos.position , hit.point,Color.magenta);
                if (innerActor.IsOneOf(_detectedActors))
                {
                    _detectedActors.Remove(innerActor);
                    OnLostEnemy?.Invoke();
                }
            }
            
            /*if (!Physics.Raycast(_eyesPos.position, targetDir, out hit, _layerMask))
            {
                innerActor.AddTo(_detectedActorsForRemove);
                continue;
            }
            if (hit.transform.GetComponent<Actor>()!=innerActor)
                innerActor.AddTo(_detectedActorsForRemove);*/
        }

        /*if (_detectedActorsForRemove.Count > 0)
        {
            foreach (var actor in _detectedActorsForRemove)
                _detectedActors.Remove(actor);
            _detectedActorsForRemove.Clear();
            OnLostEnemy?.Invoke();
        }*/

        //_meshRenderer.enabled = IsSeeEnemy;
    }
}
