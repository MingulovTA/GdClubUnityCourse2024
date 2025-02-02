using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class KeyTracker : MonoBehaviour
{
    [SerializeField] private Button _btn;
    [SerializeField] private KeyCode _key;
    private void Update() => _btn.interactable = !Input.GetKey(_key);

    private void OnValidate() => _btn = GetComponent<Button>();
}
