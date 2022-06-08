using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFactory : MonoBehaviour
{
    [SerializeField] public Material[] MaterialList;
    [SerializeField] GameObject ButtonPrefab;
    [SerializeField] List<GameObject> _buttonList = new List<GameObject>();

    public static event Action OnButtonCreation;
    
    void Awake()
    {
        ColorSender.PushMaterialList += SetMaterialList;
        StartCoroutine(PollForMaterials());

        ColorReceiverToggle.EnableAction += HideButtons;
        ColorReceiverToggle.DisableAction += ShowButtons;
        ColorReceiverToggle.ToggleAction += ButtonVisibility;

    }

    void SetMaterialList(Material[] m)
    {
        MaterialList = m;
    }

    void InstantiateButtons()
    {
        foreach (Material m in MaterialList)
        {
            var b = Instantiate(ButtonPrefab, transform);
            b.GetComponent<Image>().color = m.color;
            b.GetComponent<ColorChanger>().ImageColor = m.color;

            _buttonList.Add(b);        
        }
        
    }

    void HideButtons()
    {
        foreach (GameObject b in _buttonList)
        {
            b.SetActive(false);
        }
    }

    void ShowButtons()
    {
        foreach (GameObject b in _buttonList)
        {
            b.SetActive(true);
        }
    }

    void ButtonVisibility(bool show)
    {
        if (show == false)
            ShowButtons();
        else if (show == true)
            HideButtons();
    }

    IEnumerator PollForMaterials()
    {
        yield return new WaitUntil(delegate { return MaterialList.Length > 0; });
        InstantiateButtons();
        HideButtons();
        OnButtonCreation?.Invoke();
    }
}
