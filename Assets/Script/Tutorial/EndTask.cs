using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTask : ITutorialTask
{
    public string GetTitle()
    {
        return "�`���[�g���A�������I�I";
    }

    public string GetText()
    {
        return "�}���ŉ�ЂɌ��������I�I";
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