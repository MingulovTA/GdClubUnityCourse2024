using System;
using System.Collections.Generic;
using Arpa_common.General.Extentions;
using UnityEngine;
using UnityEngine.Events;

public class FloorButton : MonoBehaviour
{
    [SerializeField] private GameObject _active;
    [SerializeField] private GameObject _inactive;
    [SerializeField] private List<ActorClassId> _users;
    [SerializeField] private bool _isToggle;
    
    [SerializeField] private UnityEvent _onToggle;
    private bool _isActive;

    public bool IsActive => _isActive;

    public event Action OnStateChanged;

    private void OnEnable() => UpdateView();

    public void Enable()
    {
        _isActive = true;
        UpdateView();
    }

    public void Disable()
    {
        _isActive = false;
        UpdateView();
    }

    private void UpdateView()
    {
        _active.SetActive(_isActive);
        _inactive.SetActive(!_isActive);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isActive) return;
        Actor actor = other.gameObject.GetComponent<Actor>();
        if (actor==null||!actor.ActorClassId.IsOneOf(_users)) return;
        _onToggle?.Invoke();
        _isActive = true;
        UpdateView();
        OnStateChanged?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!_isActive||!_isToggle) return;
        Actor actor = other.gameObject.GetComponent<Actor>();
        if (actor==null||!actor.ActorClassId.IsOneOf(_users)) return;
        _isActive = false;
        UpdateView();
        OnStateChanged?.Invoke();
    }
}
