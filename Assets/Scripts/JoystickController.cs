using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

public class JoystickController : OnScreenControl, IPointerDownHandler, IPointerMoveHandler, IPointerUpHandler
{
    [SerializeField] private Image backgroundJoystickImage;
    [SerializeField] private Image handleJoystickImage;
    [SerializeField] private bool rotateHandle = true;
    [SerializeField, InputControl(layout = "Vector2")] private string joystickControlPath;
        
    private Vector2 _joystickPosition;
    private bool _joystickActive;
    
    protected override string controlPathInternal
    {
        get => joystickControlPath;
        set => joystickControlPath = value;
    }

    private void Start() => SetJoystickActive(false);
        
    public void OnPointerDown(PointerEventData eventData)
    {
        SetJoystickActive(true);

        _joystickPosition = eventData.position;
        backgroundJoystickImage.rectTransform.position = _joystickPosition;
        handleJoystickImage.rectTransform.position = _joystickPosition;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (!_joystickActive) return;
            
        var distance = Vector2.Distance(eventData.position, _joystickPosition);
        var maxClampedDistance = backgroundJoystickImage.rectTransform.sizeDelta.x / 2;
        var clampedDistance = Mathf.Clamp(distance, 0, maxClampedDistance);
        var direction = (eventData.position - _joystickPosition).normalized * clampedDistance;

        handleJoystickImage.rectTransform.position = _joystickPosition + direction;
        SendValueToControl(direction / maxClampedDistance);
            
        if (rotateHandle) RotateHandle(direction);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SendValueToControl(Vector2.zero);
        SetJoystickActive(false);
    }
    private void SetJoystickActive(bool value)
    {
        _joystickActive = value;
        backgroundJoystickImage.enabled = value;
        handleJoystickImage.enabled = value;
    }

    private void RotateHandle(Vector2 direction)
    {
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        handleJoystickImage.rectTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}