public class DialogAction : DayEvent
{
    public ActionType ActionType { get; set; }

    public DialogAction(int Id, string Text, ActionType ActionType) : base(Id, DayEventType.ACTION_DIALOG, Text)
    {
        this.ActionType = ActionType;
    }
}