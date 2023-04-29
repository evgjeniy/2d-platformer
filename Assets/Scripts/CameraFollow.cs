using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoCashed<Camera>
{
    [SerializeField] private List<Transform> targets;
 
    [Header("Follow Settings")]
    [SerializeField] private Vector3 offset = new(0.0f, 1.0f, -10.0f);
    [SerializeField, Range(0.01f, 3.0f)] private float smoothTime = 0.3f;
    
    [Header("Zoom Settings")]
    [SerializeField] private float minCameraSize = 4.0f;
    [SerializeField] private float maxCameraSize = 10.0f;
    [SerializeField] private float sizeMultiplier = 0.5f;

    private Vector3 _velocity = Vector3.zero;

    public void AddTarget(Transform newTarget) => targets.Add(newTarget);

    private bool RemoveTarget(Transform target) => targets.Remove(target);
    
    private void LateUpdate()
    {
        if (targets.Count == 0) return;
        
        var targetSize = Mathf.Clamp(GetGreatestDistance() * sizeMultiplier, minCameraSize, maxCameraSize);
        First.orthographicSize = Mathf.Lerp(First.orthographicSize, targetSize, Time.fixedDeltaTime * smoothTime);
        
        var targetPosition = GetCenterPoint() + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
    }

    private Vector3 GetCenterPoint()
    {
        if (targets.Count == 1) return targets[0].position;

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (var i = 1; i < targets.Count; i++) bounds.Encapsulate(targets[i].position);

        return bounds.center;
    }

    private float GetGreatestDistance()
    {
        if (targets.Count == 1) return 0f;

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (var i = 1; i < targets.Count; i++) bounds.Encapsulate(targets[i].position);

        return bounds.size.magnitude;
    }
}