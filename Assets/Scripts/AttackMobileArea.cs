using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AttackMobileArea : OnScreenControl, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField, InputControl(layout = "Button")] private string attackControlPath;

    protected override string controlPathInternal
    {
        get => attackControlPath;
        set => attackControlPath = value;
    }
    
    public void OnPointerDown(PointerEventData eventData) => SendValueToControl(1.0f);

    public void OnPointerUp(PointerEventData eventData) => SendValueToControl(0.0f);
}