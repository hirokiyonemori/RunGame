using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class MovementTask : ITutorialTask
{
    string targetTableName = "StringTable";

    public string GetTitle()
    {
        // Get
        var table = LocalizationSettings.StringDatabase.GetTable(targetTableName);

        var value = table.GetEntry("TutorialTitle1").Value;
        Debug.Log(" value " + value);


        var value2 = table.GetEntry("TutorialContents1").Value;
        Debug.Log(" value2 " + value2);

        return value+"\n" + value2;
    }

    public string GetText()
    {
        // Get
        var table = LocalizationSettings.StringDatabase.GetTable(targetTableName);

        var value = table.GetEntry("TutorialContents1").Value;
        Debug.Log(" value " + value);
        return "";
    }

    public void OnTaskSetting()
    {
        // 走る前の座標を設定する
        previousPos = currentPos;
    }

    const float LOAD_WIDTH = 6f;

    Vector3 previousPos, currentPos;

    public bool CheckTask()
    {
        if (Input.GetMouseButton(0))
        {
            // スワイプによる移動距離を取得
            currentPos = Input.mousePosition;

        }
        // 移動をした場合
        if (previousPos.x != currentPos.x)
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
