using System;
using System.Collections.Generic;
using UnityEngine;

public enum EventName
{
    OnChangeScene = 1,
    OnGetKey = 2,
    OnControllFloor = 3,
    OnJumpUp = 4,
    OnLand = 5,
    OnChangeMoveFloor = 6,
    OnRegenerationPointRef = 7,
    OnPlayerDie = 8,
    OnChangeCamera = 9,
}

public class EventSystem : MonoBehaviour
{
    public static EventSystem instance;

    private void Awake()
    {
        instance = this;
    }

    Dictionary<EventName, Delegate> eventDic = new Dictionary<EventName, Delegate>();

    /// <summary>
    /// 添加事件监听
    /// </summary>
    /// <param name="eventType">事件类型（枚举）</param>
    /// <param name="action">事件处理器</param>
    public void AddEventListener(EventName eventType, Action action)
    {
        CheckAddingEvent(eventType, action);
        eventDic[eventType] = (Action)Delegate.Combine((Action)eventDic[eventType], action);
    }

    /// <summary>
    /// 添加事件监听，但是泛型
    /// </summary>
    public void AddEventListener<T>(EventName eventType, Action<T> action)
    {
        CheckAddingEvent(eventType, action);
        eventDic[eventType] = (Action<T>)Delegate.Combine((Action<T>)eventDic[eventType], action);
    }


    /// <summary>
    /// 移除事件监听
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="action"></param>
    public void RemoveEventListener(EventName eventType, Action action)
    {
        if(CheckRemovingEvent(eventType, action))
            eventDic[eventType] = (Action)Delegate.Remove((Action)eventDic[eventType], action);
    }

    /// <summary>
    /// 移除事件监听，但是泛型
    /// </summary>
    public void RemoveEventListener<T>(EventName eventType, Action<T> action)
    {
        if (CheckRemovingEvent(eventType, action))
            eventDic[eventType] = (Action<T>)Delegate.Remove((Action<T>)eventDic[eventType], action);
    }

    /// <summary>
    /// 触发事件
    /// </summary>
    /// <param name="eventType"></param>
    /// <exception cref="Exception"></exception>
    public void EmitEvent(EventName eventType)
    {
        if (eventDic.TryGetValue(eventType, out var targetDelegate))
        {
            if (targetDelegate == null)     // 事件列表为空，无事发生
                return;

            Delegate[] invocationList = targetDelegate.GetInvocationList();

            for (int i = 0; i < invocationList.Length; i++)     // 遍历列表中的所有事件
            {
                if (invocationList[i].GetType() != typeof(Action))
                    throw new Exception($"EmitEvent{eventType} error: types of parameters are not match {typeof(Action)}.");

                Action action = (Action)invocationList[i];
                try
                {
                    action.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex.ToString());
                }
            }
        }
    }

    /// <summary>
    /// 触发事件，但是泛型
    /// </summary>
    public void EmitEvent<T>(EventName eventType, T param)
    {
        if (eventDic.TryGetValue(eventType, out var targetDelegate))
        {
            if (targetDelegate == null)     // 事件列表为空，无事发生
                return;

            Delegate[] invocationList = targetDelegate.GetInvocationList();

            for (int i = 0; i < invocationList.Length; i++)     // 遍历列表中的所有事件
            {
                if (invocationList[i].GetType() != typeof(Action<T>))
                    throw new Exception($"EmitEvent{eventType} error: types of parameters are not match {typeof(Action<T>)}.");

                Action<T> action = (Action<T>)invocationList[i];
                try
                {
                    action.Invoke(param);
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex.ToString());
                }
            }
        }
    }

    void CheckAddingEvent(EventName eventType, Delegate listener)
    {
        if (!eventDic.ContainsKey(eventType))
            eventDic.Add(eventType, null);

        Delegate tempDel = eventDic[eventType];

        if (tempDel != null && tempDel.GetType() != listener.GetType())     // 为空或类型不符合
        {
            throw new Exception(
                $"try to add incorrect eventType{eventType}. " +
                $"needed listener type is {tempDel.GetType()}, " +
                $"given listener type is {listener.GetType()}");
        }
    }

    bool CheckRemovingEvent(EventName eventType, Delegate listener)
    {
        bool result;
        if (!eventDic.ContainsKey(eventType))
            result = false;
        else
        {
            Delegate tempDel = eventDic[eventType];

            if (tempDel != null && tempDel.GetType() != listener.GetType())     // 为空或类型不符合
            {
                throw new Exception(
                    $"try to remove incorrect eventType{eventType}. " +
                    $"needed listener type is {tempDel.GetType()}, " +
                    $"given listener type is {listener.GetType()}");
            }
            result = true;
        }

        return result;
    }


}
