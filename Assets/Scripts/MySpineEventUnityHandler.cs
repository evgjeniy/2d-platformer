using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;
using AnimationState = Spine.AnimationState;

public class MySpineEventUnityHandler : MonoBehaviour
{
    [System.Serializable]
    public class EventPair
    {
        [SpineEvent] public string spineEvent;
        public UnityEvent unityHandler;
        public AnimationState.TrackEntryEventDelegate EventDelegate; 
    }

    [field: SerializeField] public List<EventPair> Events { get; private set; }
        
    private ISkeletonComponent _skeletonComponent;
    private IAnimationStateComponent _animationStateComponent;

    private void OnEnable()
    {
        _skeletonComponent ??= GetComponent<ISkeletonComponent>();
        if (_skeletonComponent == null) return;
        _animationStateComponent ??= _skeletonComponent as IAnimationStateComponent;
        if (_animationStateComponent == null) return;
        var skeleton = _skeletonComponent.Skeleton;
        if (skeleton == null) return;
            
        foreach (var eventPair in Events)
        {
            var eventData = skeleton.Data.FindEvent(eventPair.spineEvent);
            eventPair.EventDelegate ??= (_, e) =>
            {
#if UNITY_EDITOR
                    if (e.Data.Name == eventData.Name) eventPair.unityHandler.Invoke();
#else
                    if (e.Data == eventData) eventPair.unityHandler.Invoke();
#endif
            };
            _animationStateComponent.AnimationState.Event += eventPair.EventDelegate;
        }
    }

    private void OnDisable()
    {
        _animationStateComponent ??= GetComponent<IAnimationStateComponent>();
        if (_animationStateComponent == null) return;

        foreach (var eventPair in Events)
        {
            if (eventPair.EventDelegate != null) 
                _animationStateComponent.AnimationState.Event -= eventPair.EventDelegate;
                
            eventPair.EventDelegate = null;
        }
    }
}