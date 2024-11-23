using UnityEngine;
using UnityEngine.Events;

public class BaseEventListener<T> : MonoBehaviour
{
    public BaseEventSO<T> eventSO;
    public UnityEvent<T> response;

    private void OnEnable()
    {
        if (eventSO != null)
        {
            eventSO.OnEventRaised += OnEventRaised;
        }
    }
    private void OnDisable()
    {
        if (eventSO != null)
        {
            eventSO.OnEventRaised -= OnEventRaised;
        }
    }
    private void OnEventRaised(T value)
    {
        if (eventSO != null)
        {
            response.Invoke(value);
        }
    }
}
