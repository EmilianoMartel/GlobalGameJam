using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "ConfigData/ConfigData")]
public class ConfigData : ScriptableObject
{
    public List<DayAction> actions;
    public List<Dialog> dialogs;
    public List<DialogAction> dialogActions;
    public int daysToWin = 10;
    public int playerInitialBubbles = 3;
    public int playerInitialMoxie = 10;
    public int playerInitialHijinks = 10;
}
