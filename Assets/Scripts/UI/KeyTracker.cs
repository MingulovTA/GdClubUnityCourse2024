using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class KeyTracker : MonoBehaviour
{
    [SerializeField] private Button _btn;
    [SerializeField] private List<KeyCode> _key;
    private void Update()
    {
        foreach (var keyCode in _key)
        {
            if (Input.GetKey(keyCode))
            {
                _btn.interactable = false;
                return;
            }
        }

        _btn.interactable = true;
    }

    private void OnValidate() => _btn = GetComponent<Button>();
}
