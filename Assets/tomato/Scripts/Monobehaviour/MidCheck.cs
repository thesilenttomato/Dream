using System;
using UnityEngine;

public class MidCheck : MonoBehaviour
{
    public IntVarible hp;
    public IntVarible hour;
    public IntVarible failTime;
    public ObjectEventSO rousedEvent;
    public ObjectEventSO remindEvent;
    public IntEventSO EndEvent;
    public IntVarible TheEndGame;
    private void Awake()
    {
        if (TheEndGame.currentVaule == 1)
        {
            EndEvent.RaiseEvent(2,this);
            return;
        }
        if (hp.currentVaule <= 0)
        {
            if (hour.currentVaule >= 7)
            {
                EndEvent.RaiseEvent(3,this);
            }
            failTime.currentVaule += 1;
            if (failTime.currentVaule >= 3)
            {
                EndEvent.RaiseEvent(1,this);
                return;
            }
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
