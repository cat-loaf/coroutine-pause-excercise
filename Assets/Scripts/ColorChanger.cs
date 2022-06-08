using System;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    public static event Action<Color> ChangeColor;

    [SerializeField] public Color ImageColor;
    Button _button;

    void Start()
    {
        _button = GetComponent<Button>();

        _button.onClick.AddListener(delegate { ChangeColor.Invoke(ImageColor); });
    }

}
