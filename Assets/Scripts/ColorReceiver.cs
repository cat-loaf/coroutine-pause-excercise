using UnityEngine;

public class ColorReceiver : MonoBehaviour
{
    Material _originalMaterial;
    MeshRenderer _renderer;

    void Awake() 
    {
        _renderer = GetComponent<MeshRenderer>();
        _originalMaterial = _renderer.material;

        ColorSender.UpdateColor += ChangeColor;
        ColorSender.ResetColor += ResetColor;
        ButtonFactory.OnButtonCreation += SubscribeToColorChanger;
    }
    
    void ChangeColor(Material m) 
    {
        _renderer.material = m;
    }

    void ChangeColor(Color c)
    {
        Material newm = new Material(_originalMaterial);
        newm.color = c;
        _renderer.material = newm;
    }

    void ResetColor() 
    {
        _renderer.material = _originalMaterial;
    }
    
    public void SubscribeToColorChanger()
    {
        ColorChanger.ChangeColor += ChangeColor;
    }


}
