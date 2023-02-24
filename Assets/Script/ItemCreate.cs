using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemCreate : MonoBehaviour
{
    //carPrefabを入れる
    public GameObject carPrefab;
    //coinPrefabを入れる
    public GameObject coinPrefab;
    //cornPrefabを入れる
    public GameObject conePrefab;

    //ステージチップの配列
    public GameObject[] stageTips;

    //int型を変数StageTipSizeで宣言します。
    const int STAGE_TIP_SIZE = 32;

    //アイテムを出すx方向の範囲
    private float posRange = 3.4f;
    //作ったステージを削除
    public List<GameObject> generatedStageList = new List<GameObject>();

    //ターゲットキャラクターの指定が出来る様にするよ
    public Transform character;

    //自動生成する時に使う変数startTipIndex
    public int startTipIndex;
    //ステージ生成の先読み個数
    public int preInstantiate;

    const int CAR = 0;
    const int COIN = 1;
    const int CONE = 2;

    //int型を変数currentTipIndexで宣言します。
    int currentTipIndex;

    private void Start()
    {
        Debug.Log(" preInstantiate " + preInstantiate );
    }

    // Update is called once per frame
    void Update()
    {


        //キャラクターの位置から現在のステージチップのインデックスを計算します
        int charaPositionIndex = (int)(character.position.z / STAGE_TIP_SIZE);
        //次のステージチップに入ったらステージの更新処理を行います。
        if (charaPositionIndex + preInstantiate > currentTipIndex)
        {
            UpdateStage(charaPositionIndex + preInstantiate);
        }

    }

    

    private int[] carTable = {2,3,4,5 };
    private int[] coinTable = {7,6,7,7 };
    private int[] coneTable = {10,10,10,10 };
    
    //指定のインデックスまでのステージチップを生成して、管理下におく
    void UpdateStage(int toTipIndex)
    {
        if (toTipIndex <= currentTipIndex) return;

        //指定のステージチップまで生成するよ
        for (int i = currentTipIndex + 1; i <= toTipIndex; i++)
        {
            
            //どのアイテムを出すのかをランダムに設定
            int num = Random.Range(0, 10);
            int nextStageTip = 0;
            int tableNo = PlayerManager.instance.stageNo;
            if (num <= carTable[tableNo])
            {
                nextStageTip = CAR;
            }else if (num <= coinTable[tableNo])
            {
                nextStageTip = COIN;
            }else if (num <= coneTable[tableNo])
            {
                nextStageTip = CONE;
            }

            //int nextStageTip = Random.Range(0, stageTips.Length);
            int tipIndex = i;
            int rot = 0;
            int rotY = 0;
            // 車の場合は回転処理を加える
            if (nextStageTip == CAR)
            {
                if (PlayerManager.instance.stageNo >= 3)
                {
                    rotY = 180;
                }
                
                rot = -90;   
                
            }
            float pos_y = 1;
            //次表示するチップ
            switch (nextStageTip)
            {
                case CAR:
                    pos_y = 0.2f;
                    break;
                case COIN:
                    pos_y = 1;
                    break;
                case CONE:
                    pos_y = 0;
                    break;

            }
            

            //一定の距離ごとにアイテムを生成
            
            int posX = Random.Range(-2, 1);
            int count = 0;
            if (nextStageTip == CONE)
            {
                posX = -4;
                count = 10;
                //コーンをx軸方向に一直線に生成
                // 初級の場合は真ん中に配置
                if (PlayerManager.instance.stageNo == 0)
                {
                    count = 1;
                    posX = 0;
                }
                for (int j = 0; j < count; j ++)
                {
                    
                    GameObject stageObject = (GameObject)Instantiate(
                            stageTips[nextStageTip],
                            new Vector3(posX + j, pos_y, tipIndex * STAGE_TIP_SIZE),
                            Quaternion.Euler(rot, 0, 0));
                    //生成したステージチップを管理リストに追加して、
                    generatedStageList.Add(stageObject);
                }
            }
            else
            {
                count = 2 + 2 * PlayerManager.instance.stageNo;
                //レーンごとにアイテムを生成
                
                if (nextStageTip == COIN)
                {
                    for (int j = 0; j < count; j++)
                    {
                        //車を生成
                        GameObject stageObject = (GameObject)Instantiate(
                            stageTips[nextStageTip],
                            new Vector3(posX , pos_y, tipIndex * STAGE_TIP_SIZE + j * 10 ),
                            Quaternion.Euler(rot, 0, 0));

                        //生成したステージチップを管理リストに追加して、
                        generatedStageList.Add(stageObject);
                    }
                }else if (nextStageTip == CAR)
                {
                    int carCount = PlayerManager.instance.stageNo + 1;
                    if (carCount >= 3) carCount = 3;
                
                    //レーンごとにアイテムを生成
                    for (int j = 0; j < carCount; j++)
                    {
                    
                    
                        //コインを生成
                        GameObject stageObject = (GameObject)Instantiate(
                            stageTips[nextStageTip],
                            new Vector3(posX + (j * 4  ) , pos_y, tipIndex * STAGE_TIP_SIZE),
                            Quaternion.Euler(rot, rotY, 0));
                        //生成したステージチップを管理リストに追加して、
                        generatedStageList.Add(stageObject);
                    }   
                }
            }
            //GameObject stageObject = GenerateStage(i);
            //生成したステージチップを管理リストに追加して、
            //generatedStageList.Add(stageObject);
        }
        //ステージ保持上限になるまで古いステージを削除します。
        
        while (generatedStageList.Count > preInstantiate + 4 + 50 * PlayerManager.instance.stageNo ) DestroyOldestStage();
        //while (generatedStageList.Count > preInstantiate + 4 + 4 * PlayerManager.instance.stageNo) DestroyOldestStage();

        currentTipIndex = toTipIndex;

    }

    //一番古いステージを削除します
    void DestroyOldestStage()
    {
        GameObject oldStage = generatedStageList[0];
        generatedStageList.RemoveAt(0);
        Destroy(oldStage);
    }
}
