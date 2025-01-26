using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Days", menuName = "Day/Day") ]
public class Day : ScriptableObject
{
    
    [SerializeField]public int Id; //THIS IS UNIQUE
    [SerializeField]public int Day_ID; //THIS IS TO IDENTIFY THE DAY (IN TE CASE OF DIALOG ACTIONS)
    [SerializeField]public string Type;
    [SerializeField]public string Text;
    [SerializeField]public string ActionType;
    [SerializeField]public string ResponseAction1;
    [SerializeField]public string ResponseAction2;
    [SerializeField]public string Amount1;
    [SerializeField]public string Amount2;
}
