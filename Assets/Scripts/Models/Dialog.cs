public class Dialog : DayEvent
{
    public DialogAction FirstAction { get; set; }
    public DialogAction SecondAction { get; set; }

    public int Day_ID;

    public DayEventType DayEventType;

    public Dialog(int Day_ID, int Id, string Text, DialogAction FirstAction, DialogAction SecondAction) : base(Id, DayEventType.DIALOG, Text)
    {
        this.FirstAction = FirstAction;
        this.SecondAction = SecondAction;
        this.Day_ID = Day_ID;
    }
}
