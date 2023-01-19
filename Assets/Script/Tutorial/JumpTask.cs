using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTask : ITutorialTask
{
    public string GetTitle()
    {
        return "基本操作 ジャンプ (2/2)";
    }

    public string GetText()
    {
        return "画面をタッチし離すとジャンプすることができる";
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