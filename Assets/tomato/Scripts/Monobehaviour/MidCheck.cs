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
    public EmoLibrary playerEmo;
    private void Awake()
    {
        //结局
        if (TheEndGame.currentVaule == 1)
        {
            EndEvent.RaiseEvent(2,this);
            return;
        }
        //死了
        
        if (hp.currentVaule <= 0)
        {
            hp.maxVaule += 1;
            if (lazy.currentVaule >= 2 && hour.currentVaule >=7 && hour.currentVaule <= 12)
            {
                EndEvent.RaiseEvent(5,this);
                return;
            }else if (happy.currentVaule >= 2 && hour.currentVaule >=7 && hour.currentVaule <= 12)
            {
                EndEvent.RaiseEvent(7,this);
                return;
            }
            if (hour.currentVaule >= 6 && hour.currentVaule <= 12)
            {
               // ReduceEmo();
            }
            if (hour.currentVaule >= 7 && hour.currentVaule <= 12)
            {
                EndEvent.RaiseEvent(3,this);
                return;
            }
            failTime.currentVaule += 1;
            if (failTime.currentVaule >= 3 && hour.currentVaule >=6 && hour.currentVaule <= 12)
            {
                EndEvent.RaiseEvent(1,this);
                return;
            }
            rousedEvent.RaiseEvent(null,this);
        }
        //时间到了
        else
        {
            
            if (hour.currentVaule >= 8  && hour.currentVaule <= 12)
            {
                if (lazy.currentVaule >= 2)
                {
                    EndEvent.RaiseEvent(4,this);
                    return;
                }else if (happy.currentVaule >= 2)
                {
                    EndEvent.RaiseEvent(6,this);
                    return;
                }
                EndEvent.RaiseEvent(0,this);
            }
            else
            {
                if (hour.currentVaule >= 7 && hour.currentVaule <= 12)
                {
                   //ReduceEmo();
                }
                remindEvent.RaiseEvent(null,this);
            }
            
        }
    }

    
}
