using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTask : ITutorialTask
{
    public string GetTitle()
    {
        return "基本操作 移動(1/2)";
    }

    public string GetText()
    {
        return "スワイプでキャラクターを移動することができます。";
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
