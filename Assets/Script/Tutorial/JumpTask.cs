using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTask : ITutorialTask
{
    public string GetTitle()
    {
        return "��{���� �W�����v (2/2)";
    }

    public string GetText()
    {
        return "��ʂ��^�b�`�������ƃW�����v���邱�Ƃ��ł���";
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