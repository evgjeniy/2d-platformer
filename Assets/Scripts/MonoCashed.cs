using UnityEngine;

public abstract class MonoCashed : MonoBehaviour
{
    public new Transform transform { get; private set; }
    public new GameObject gameObject { get; private set; }
    
    public Vector3 position
    {
        get => transform.position;
        set => transform.position = value;
    }

    public Quaternion rotation
    {
        get => transform.rotation;
        set => transform.rotation = value;
    }

    public Vector3 localScale
    {
        get => transform.localScale;
        set => transform.localScale = value;
    }

    protected virtual void Awake()
    {
        transform = base.transform;
        gameObject = base.gameObject;
        PostAwake();
    }
    
    protected virtual void PostAwake() {}
}

public abstract class MonoCashed<T> : MonoCashed where T : Component
{
    public T First { get; private set; }
    protected override void Awake() { First = GetComponent<T>(); base.Awake(); }
}

public abstract class MonoCashed<T1, T2> : MonoCashed<T1> where T1 : Component where T2 : Component
{
    public T2 Second { get; private set; }
    protected override void Awake() { Second = GetComponent<T2>(); base.Awake(); }
}

public abstract class MonoCashed<T1, T2, T3> : MonoCashed<T1, T2> where T1 : Component where T2 : Component where T3 : Component
{
    public T3 Third { get; private set; }
    protected override void Awake() { Third = GetComponent<T3>(); base.Awake(); }
}

public abstract class MonoCashed<T1, T2, T3, T4> : MonoCashed<T1, T2, T3> where T1 : Component where T2 : Component where T3 : Component where T4 : Component
{
    public T4 Fourth { get; private set; }
    protected override void Awake() { Fourth = GetComponent<T4>(); base.Awake(); }
}