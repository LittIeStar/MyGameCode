using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    
    private Dictionary<string, Action<Dictionary<string, object>>> eventHandlers;

    private void Awake()
    {
        eventHandlers = new Dictionary<string, Action<Dictionary<string, object>>>();

        // 示例注册（也可以在别处注册）
        RegisterHandler("PauseGame", (p) => { Time.timeScale = 0; });
        RegisterHandler("PlayBGM", (p) =>
        {
            if (TryGetParam<string>(p, "clipName", out var clipName))
            {
                Debug.Log("播放音乐: " + clipName);
                // AudioManager.Instance.Play(clipName);
            }
            else
            {
                Debug.LogWarning("PlayBGM 缺少 clipName 参数");
            }
        });
        RegisterHandler("SpawnEnemy", (p) =>
        {
            if (TryGetParam<string>(p, "enemyId", out var enemyId) && TryGetParam<int>(p, "count", out var count))
            {
                Debug.Log($"生成 {count} 个敌人: {enemyId}");
                // Spawn logic...
            }
            else
            {
                Debug.LogWarning("SpawnEnemy 参数错误");
            }
        });
    }

    // 注册／注销接口
    public void RegisterHandler(string eventId, Action<Dictionary<string, object>> handler)
    {
        if (string.IsNullOrEmpty(eventId) || handler == null) return;

        if (eventHandlers.TryGetValue(eventId, out var existing))
            eventHandlers[eventId] = existing + handler; // 合并 delegate（多订阅者）
        else
            eventHandlers[eventId] = handler;
    }

    public void UnregisterHandler(string eventId, Action<Dictionary<string, object>> handler)
    {
        if (string.IsNullOrEmpty(eventId) || handler == null) return;

        if (eventHandlers.TryGetValue(eventId, out var existing))
        {
            var updated = existing - handler;
            if (updated == null)
                eventHandlers.Remove(eventId);
            else
                eventHandlers[eventId] = updated;
        }
    }

    public void TriggerEvent(string eventId, Dictionary<string, object> parameters = null)
    {
        if (string.IsNullOrEmpty(eventId)) return;

        if (eventHandlers.TryGetValue(eventId, out var handlers))
        {
            try
            {
                handlers?.Invoke(parameters);
            }
            catch (Exception ex)
            {
                Debug.LogError($"事件 {eventId} 执行出错: {ex}");
            }
        }
        else
        {
            Debug.LogWarning("未找到事件：" + eventId);
        }
    }

    // 参数读取帮助方法（安全转换，支持 int/float/string 等）
    public static bool TryGetParam<T>(Dictionary<string, object> parameters, string key, out T value)
    {
        value = default;
        if (parameters == null || string.IsNullOrEmpty(key)) return false;
        if (!parameters.TryGetValue(key, out var obj) || obj == null) return false;

        // 直接类型匹配
        if (obj is T t) { value = t; return true; }

        // 尝试常用转换（string -> int/float 等）
        try
        {
            var targetType = typeof(T);
            var converted = Convert.ChangeType(obj, targetType);
            if (converted is T cv) { value = cv; return true; }
        }
        catch
        {
            // 忽略转换异常
        }
        return false;
    }
}
