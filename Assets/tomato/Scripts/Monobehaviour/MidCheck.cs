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
    public IntVarible happy;
    public IntVarible lazy;
    private void Awake()
    {
        if (TheEndGame.currentVaule == 1)
        {
            EndEvent.RaiseEvent(2,this);
            return;
        }
        if (hp.currentVaule <= 0)
        {
            if (lazy.currentVaule >= 3)
            {
                EndEvent.RaiseEvent(5,this);
                return;
            }else if (happy.currentVaule >= 3)
            {
                EndEvent.RaiseEvent(7,this);
                return;
            }
            if (hour.currentVaule >= 7)
            {
                EndEvent.RaiseEvent(3,this);
                return;
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
                if (lazy.currentVaule >= 3)
                {
                    EndEvent.RaiseEvent(4,this);
                    return;
                }else if (happy.currentVaule >= 3)
                {
                    EndEvent.RaiseEvent(6,this);
                    return;
                }
                EndEvent.RaiseEvent(0,this);
            }
            else
            {
                remindEvent.RaiseEvent(null,this);
            }
            
        }
    }
}
