using System;
using UnityEngine;

public class MidCheck : MonoBehaviour
{
    public IntVarible hp;
    public IntVarible hour;
    public ObjectEventSO rousedEvent;
    public ObjectEventSO remindEvent;
    public IntEventSO EndEvent;
    private void Awake()
    {
        if (hp.currentVaule <= 0)
        {
            rousedEvent.RaiseEvent(null,this);
        }
        else
        {
            if (hour.currentVaule >= 8  && hour.currentVaule <= 12)
            {
                EndEvent.RaiseEvent(0,this);
            }
            else
            {
                remindEvent.RaiseEvent(null,this);
            }
            
        }
    }
}
