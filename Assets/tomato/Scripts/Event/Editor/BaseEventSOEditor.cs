using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BaseEventSO<>))]
public class BaseEventSOEditor<T> : Editor
{
    private BaseEventSO<T> BaseEventSO ;
    private void OnEnable()
    {
        if (BaseEventSO == null)
        {
           
            BaseEventSO = target as BaseEventSO<T>;
           
        }
       
    }
    public override void OnInspectorGUI()
    {
        
        base.OnInspectorGUI();
        EditorGUILayout.LabelField("订阅对象:" + GetListeners().Count);
        foreach (var listener in GetListeners())
        {
            EditorGUILayout.LabelField(listener.ToString());
        }
    }

    private List<MonoBehaviour> GetListeners()
    {
        
        List<MonoBehaviour> listeners = new();
        if (BaseEventSO == null || BaseEventSO.OnEventRaised == null)
        {
            return listeners;
        }
        var subscribers = BaseEventSO.OnEventRaised.GetInvocationList();
        
        foreach (var subscriber in subscribers )
        {
            var obj = subscriber.Target as MonoBehaviour;
            if (!listeners.Contains(obj))
            {
                listeners.Add(obj);
            }
        }
        return listeners;
    }
}