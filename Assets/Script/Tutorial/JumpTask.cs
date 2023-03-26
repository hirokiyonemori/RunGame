using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class JumpTask : ITutorialTask
{
    string targetTableName = "StringTable";

    public string GetTitle()
    {

        // Get
        var table = LocalizationSettings.StringDatabase.GetTable(targetTableName);

        var value = table.GetEntry("TutorialTitle2").Value;


        var value2 = table.GetEntry("TutorialContents2").Value;
        Debug.Log(" value " + value);

        Debug.Log(" value " + value2);
        return value +"\n" +  value2;
    }

    public string GetText()
    {
        

        // Get
        var table = LocalizationSettings.StringDatabase.GetTable(targetTableName);

        var value = table.GetEntry("TutorialContents2").Value;
        Debug.Log(" value " + value);
        return "";
    }

    public void OnTaskSetting()
    {
    }

    public bool CheckTask()
    {
        if (Input.GetMouseButtonUp(0))
        {
            return true;
        }

        return false;
    }

    public float GetTransitionTime()
    {
        return 2f;
    }
}