using System;

[Serializable]
public class DialogAction : DayEvent
{
    public ActionType ActionType { get; set; }

    public DialogAction(int Id, string Text, ActionType ActionType) : base(Id, DayEventType.DIALOG_ACTION, Text)
    {
        this.ActionType = ActionType;
    }
}