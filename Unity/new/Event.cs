using UnityEngine;

[System.Serializable]
public class Event
{
    public int lineIndex;               // 触发事件的文本下标
    public EventType eventType; // 事件类型
    public string eventParam;           // 事件参数（可选，比如音效名、场景名等）
}

public enum EventType
{
    Pause,
    PlaySound,
    ChangeScene,
    Custom // 可扩展
}

[CreateAssetMenu(fileName = "EventData", menuName = "Game/Event Data")]
public class EventData : ScriptableObject
{
    public Event[] events;
}
