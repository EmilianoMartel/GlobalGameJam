public class Action : DayEvent
{
    public ActionType ActionType { get; set; }

    public Action(int Id, string Text, ActionType ActionType) : base(Id, DayEventType.ACTION, Text) 
    {
        this.ActionType = ActionType;
    }
}
