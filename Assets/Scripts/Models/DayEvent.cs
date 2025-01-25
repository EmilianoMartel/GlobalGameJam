public class DayEvent
{
    public int Id { get; protected set; }
    public DayEventType Type { get; protected set; }
    public string Text { get; protected set; }

    public DayEvent(int Id, DayEventType Type, string Text)
    {
        this.Id = Id;
        this.Type = Type;
        this.Text = Text;
    }
}
