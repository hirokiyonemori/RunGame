using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTask : ITutorialTask
{
    public string GetTitle()
    {
        return "チュートリアル完了！！";
    }

    public string GetText()
    {
        return "急いで会社に向かおう！！";
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