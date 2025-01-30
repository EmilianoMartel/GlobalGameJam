using System;
using UnityEngine;

[Serializable]
public class DialogAction : DayEvent
{
    [SerializeField] private ActionType actionType;

    public ActionType ActionType => actionType;

    public void Initialize(int id, string text, ActionType actionType)
    {
        base.Initialize(id, DayEventType.DIALOG_ACTION, text);
        this.actionType = actionType;
    }
}