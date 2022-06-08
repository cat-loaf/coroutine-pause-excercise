using System.Collections;
using System;
using UnityEngine;

public class ColorSender : MonoBehaviour
{
    [SerializeField] Material[] MaterialList;
    [SerializeField] bool ActiveStatus;
    [SerializeField] float _waitTime = 0.25f;

    public static event Action<Material> UpdateColor;
    public static event Action ResetColor;
    public static event Action<Material[]> PushMaterialList;
    void Awake()
    {
        ColorReceiverToggle.ToggleAction += ChangeActiveState;
        PushMaterialList?.Invoke(MaterialList);
        StartCoroutine(SequenceColors());
    }

    void ChangeActiveState(bool a) 
    {
        ActiveStatus = a;
    }

    IEnumerator SequenceColors() {
        while (true)
        {
            
            if (MaterialList.Length >= 0)
            {
                foreach (Material mat in MaterialList)
                {
                    if (ActiveStatus == false)
                    {
                        ResetColor?.Invoke();
                        PushMaterialList?.Invoke(MaterialList);
                        yield return new WaitUntil(() => ActiveStatus == true);
                    }
                    else 
                    {
                        UpdateColor?.Invoke(mat);
                        PushMaterialList?.Invoke(MaterialList);
                        yield return new WaitForSecondsRealtime(_waitTime);
                    }
                }
            }
        }
    }
}
