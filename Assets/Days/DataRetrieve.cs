using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleSheetsToUnity;
using System.Linq;
using Unity.VisualScripting;
using System;



#if UNITY_EDITOR
using UnityEditor;
#endif


[CreateAssetMenu(fileName = "Days", menuName = "Day/DataRetrieve") ]

public class DataRetrieve : ScriptableObject
{
    
    [SerializeField] public List<Day> days_list = new List<Day>();
    [SerializeField] public int last_days_amount = 0;
    
    public void GetData()
    {
        Debug.Log("DATA RETRIEVE");        

        ResetData();
        SpreadsheetManager.Read(new GSTU_Search("1rCXe1TjigvgZ8S4XdGoupylYFF9jDkiTZqf0_bbmas0", "DaysData"), WhenSpreadsheetDataReady);
    }

    public void ResetData(){
        var toDelete  = new string[last_days_amount];
        var outFailedPaths = new List<string>();
        for (var i=0; i < last_days_amount; i++){
            toDelete[i] = "Assets/Days/SCriptableObjects/Day_" + i;
        }
        AssetDatabase.DeleteAssets(toDelete, outFailedPaths);

        days_list = new List<Day>();        
    }

    private void WhenSpreadsheetDataReady(GstuSpreadSheet spreadSheetRef){

        //GSTU_Cell cell = spreadSheetRef["A2"];
        List<GSTU_Cell> day_ids = spreadSheetRef.columns["Day_ID"];
        List<GSTU_Cell> types = spreadSheetRef.columns["Type"];
        List<GSTU_Cell> texts = spreadSheetRef.columns["Text"];
        List<GSTU_Cell> actionTypes = spreadSheetRef.columns["ActionType"];
        List<GSTU_Cell> response1 = spreadSheetRef.columns["ResponseAction1"];
        List<GSTU_Cell> response2 = spreadSheetRef.columns["ResponseAction1"];
        List<GSTU_Cell> amount1 = spreadSheetRef.columns["Amount1"];
        List<GSTU_Cell> amount2 = spreadSheetRef.columns["Amount2"];
        
        for (int i = 1; i < types.Count; i++)
        {
            var day = CreateInstance<Day>();
            day.Id = i;
            day.Day_ID = int.Parse(day_ids[i].value);
            day.Type = types[i].value;
            day.Text = texts[i].value;
            day.ActionType = actionTypes[i].value;
            day.ResponseAction1 = response1[i].value;
            day.ResponseAction2 = response2[i].value;
            day.Amount1 = amount1[i].value;
            day.Amount2 = amount2[i].value;

            days_list.Add( day );

            last_days_amount = day.Day_ID;
            
            AssetDatabase.CreateAsset(day, "Assets/Days/ScriptableObjects/Day_" + i + ".asset");
        }
    }
}
