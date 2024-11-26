using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "YesterdayEvent", menuName = "yeseterday/YesterdayEvent")]
public class YesterdayLibrary : ScriptableObject
{
   public List<YesterDayEventSO> dayEvents_1st = new List<YesterDayEventSO>();
   public List<YesterDayEventSO> dayEvents_2nd = new List<YesterDayEventSO>();
   public List<YesterDayEventSO> dayEvents_3rd = new List<YesterDayEventSO>();
   public List<YesterDayEventSO> dayEvents_4th = new List<YesterDayEventSO>();
   public List<YesterDayEventSO> dayEvents_5th = new List<YesterDayEventSO>();
}
