using System;
using UnityEngine;

[Serializable]
public class DayEvent
{
    [SerializeField] protected int id;
    [SerializeField] protected DayEventType type;
    [SerializeField] protected string text;

    public int Id => id;
    public DayEventType Type => type;
    public string Text => text;

    public void Initialize(int id, DayEventType type, string text)
    {
        this.id = id;
        this.type = type;
        this.text = text;
    }
}
