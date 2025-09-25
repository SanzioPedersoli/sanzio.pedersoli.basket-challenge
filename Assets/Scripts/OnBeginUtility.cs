using UnityEngine;
using UnityEngine.Events;

public class OnBeginUtility : MonoBehaviour
{
    public UnityEvent AwakeCalled;
    public UnityEvent StartCalled;

    private void Awake()
    {
        AwakeCalled?.Invoke();
    }

    void Start()
    {
        StartCalled?.Invoke();
    }
}
