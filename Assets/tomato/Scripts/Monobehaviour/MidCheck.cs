using System;
using UnityEngine;

public class MidCheck : MonoBehaviour
{
    public IntVarible hp;
    public ObjectEventSO rousedEvent;
    public ObjectEventSO remindEvent;
    private void Awake()
    {
        if (hp.currentVaule <= 0)
        {
            rousedEvent.RaiseEvent(null,this);
        }
        else
        {
            remindEvent.RaiseEvent(null,this);
        }
    }
}
