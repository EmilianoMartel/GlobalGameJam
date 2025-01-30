using UnityEngine;
using System;

[Serializable]
public class Dialog : DayEvent
{
    public DialogAction FirstAction;
    public DialogAction SecondAction;

    public int NPC_ID;

    public void Initialize(int id, string text, DialogAction firstAction, DialogAction secondAction, string npc_id)
    {
        this.id = id;
        this.text = text;
        this.type = DayEventType.DIALOG;
        this.FirstAction = firstAction;
        this.SecondAction = secondAction;

        if (npc_id != "None")
        {
            this.NPC_ID = int.Parse(npc_id);
        }
    }
}
