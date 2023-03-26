using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class EndTask : ITutorialTask
{
    string targetTableName = "StringTable";


    public string GetTitle()
    {
        // Get
        var table = LocalizationSettings.StringDatabase.GetTable(targetTableName);

        var value = table.GetEntry("TutorialTitleEnd").Value;
        Debug.Log(" value " + value);
        

        var value2 = table.GetEntry("TutorialContentsEnd").Value;
        Debug.Log(" value2 " + value2);

        return value + "\n"+ value2;
    }

    public string GetText()
    {
        return "";
    }

    public void OnTaskSetting()
    {
    }

    public bool CheckTask()
    {
        if (Input.GetMouseButtonDown(0))
        {
            return true;
        }

        return false;
    }

    public float GetTransitionTime()
    {
        return 1f;
    }
}