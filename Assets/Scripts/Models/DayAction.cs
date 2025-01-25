public class DayAction : DayEvent
{
    public ActionType ActionType { get; set; }

    public DayAction(int Id, string Text, ActionType ActionType) : base(Id, DayEventType.ACTION, Text) 
    {
        this.ActionType = ActionType;
    }
}
