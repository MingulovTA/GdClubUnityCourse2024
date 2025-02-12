using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class Door : MonoBehaviour
{
    private const float OPEN_CLOSED_TIME = 1f;
    
    [SerializeField] private List<Transform> _flaps;
    [SerializeField] private List<FloorButton> _masterButtons;
    [SerializeField] private Collider2D _collider2D;

    private bool CanOpen => _masterButtons.All(mb => mb.IsActive);

    private bool _isOpen;

    private void OnEnable()
    {
        foreach (var masterButton in _masterButtons)
            masterButton.OnStateChanged += MasterButtonStateChangeHandler;
        UpdateView();
    }

    private void OnDisable()
    {
        foreach (var masterButton in _masterButtons)
            masterButton.OnStateChanged -= MasterButtonStateChangeHandler;
    }

    private void UpdateView()
    {
        _isOpen = CanOpen;
        foreach (var flap in _flaps)
            flap.localScale = CanOpen ? new Vector3(0,1,1) : Vector3.one;
        _collider2D.enabled = !_isOpen;
    }
    private void MasterButtonStateChangeHandler()
    {
        if (!_isOpen)
        {
            if (CanOpen)
                Open();
        }
        else
        {
            if (!CanOpen)
                Close();
        }
    }

    private void Open()
    {
        foreach (var flap in _flaps)
            flap.DOScaleX(0, OPEN_CLOSED_TIME).OnComplete(UpdateView);
    }

    private void Close()
    {
        foreach (var flap in _flaps)
            flap.DOScaleX(1, OPEN_CLOSED_TIME).OnComplete(UpdateView);
    }
}
