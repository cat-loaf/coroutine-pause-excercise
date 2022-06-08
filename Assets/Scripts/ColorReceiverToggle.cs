using System;
using UnityEngine;
using UnityEngine.UI;

public class ColorReceiverToggle : MonoBehaviour
{
    public static event Action<bool> ToggleAction;
    public static event Action DisableAction;
    public static event Action EnableAction;

    Toggle _colorToggle;
    
    void Awake() 
    {
        _colorToggle = GetComponent<Toggle>();
        _colorToggle.onValueChanged.AddListener(delegate { ToggleAction?.Invoke(_colorToggle.isOn); });

        EnableAction += delegate { ToggleAction?.Invoke(_colorToggle.isOn); };
        DisableAction += delegate { ToggleAction?.Invoke(false); };

    }

    void OnDisable()
    {
        DisableAction?.Invoke();
    }
    
    void OnEnable()
    {
        EnableAction?.Invoke();
    }
}
