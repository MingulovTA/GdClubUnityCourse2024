using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseActor : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private ActorClassId _actorClassId;

    public ActorClassId ActorClassId => _actorClassId;
}
