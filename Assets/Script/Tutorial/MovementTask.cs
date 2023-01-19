using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTask : ITutorialTask
{
    public string GetTitle()
    {
        return "��{���� �ړ�(1/2)";
    }

    public string GetText()
    {
        return "�X���C�v�ŃL�����N�^�[���ړ����邱�Ƃ��ł��܂��B";
    }

    public void OnTaskSetting()
    {
        // ����O�̍��W��ݒ肷��
        previousPos = currentPos;
    }

    const float LOAD_WIDTH = 6f;

    Vector3 previousPos, currentPos;

    public bool CheckTask()
    {
        if (Input.GetMouseButton(0))
        {
            // �X���C�v�ɂ��ړ��������擾
            currentPos = Input.mousePosition;

        }
        // �ړ��������ꍇ
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
