using System;
using System.Collections.Generic;
using Arpa_common.General.Extentions;
using UnityEngine;

public class FloorButton : MonoBehaviour
{
    [SerializeField] private GameObject _active;
    [SerializeField] private GameObject _inactive;
    [SerializeField] private List<ActorClassId> _users;
    [SerializeField] private bool _isToggle;
    
    private bool _isActive;

    public bool IsActive => _isActive;

    public event Action OnStateChanged;

    private void OnEnable() => UpdateView();

    private void UpdateView()
    {
        _active.SetActive(_isActive);
        _inactive.SetActive(!_isActive);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isActive) return;
        BaseActor baseActor = other.gameObject.GetComponent<BaseActor>();
        if (baseActor==null||!baseActor.ActorClassId.IsOneOf(_users)) return;
        _isActive = true;
        UpdateView();
        OnStateChanged?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!_isActive||!_isToggle) return;
        BaseActor baseActor = other.gameObject.GetComponent<BaseActor>();
        if (baseActor==null||!baseActor.ActorClassId.IsOneOf(_users)) return;
        _isActive = false;
        UpdateView();
        OnStateChanged?.Invoke();
    }
}
