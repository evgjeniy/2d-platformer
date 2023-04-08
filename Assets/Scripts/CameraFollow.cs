using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoCashed<Camera>
{
    [SerializeField] private List<Transform> targets;
    [SerializeField] private Vector3 offset;
    [SerializeField, Range(0.01f, 3.0f)] private float smoothTime = 0.3f;
    
    [Space]
    [SerializeField] private float maxDistance = 10.0f;
    [SerializeField] private float minSize = 5.0f;
    [SerializeField] private float maxSize = 10.0f;
    [SerializeField] private float sizePadding = 1.0f;

    private Vector3 _velocity = Vector3.zero;

    private void LateUpdate()
    {
        if (targets.Count == 0) return;

        var centerPoint = GetCenterPoint();
        var distance = GetGreatestDistance();

        if (distance > maxDistance)
        {
            var targetSize = Mathf.Clamp(distance + sizePadding, minSize, maxSize);
            First.orthographicSize = Mathf.Lerp(First.orthographicSize, targetSize, Time.deltaTime * smoothTime);

            var targetPosition = centerPoint + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
        }
        else
        {
            var targetPosition = centerPoint + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
            First.orthographicSize = Mathf.Lerp(First.orthographicSize, minSize, Time.deltaTime * smoothTime);
        }
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