using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCreate : MonoBehaviour
{
    //carPrefabを入れる
    public GameObject carPrefab;
    //coinPrefabを入れる
    public GameObject coinPrefab;
    //cornPrefabを入れる
    public GameObject conePrefab;
    //スタート地点
    private int startPos = -160;
    //ゴール地点
    private int goalPos = 120;

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



    // Use this for initialization
    void Start()
    {
        /*//一定の距離ごとにアイテムを生成
        for (int i = startPos; i < goalPos; i += 15)
        {
            //どのアイテムを出すのかをランダムに設定
            int num = Random.Range(0, 10);
            if (num <= 1)
            {
                //コーンをx軸方向に一直線に生成
                for (float j = -1; j <= 1; j += 0.4f)
                {
                    GameObject cone = Instantiate(conePrefab) as GameObject;
                    cone.transform.position = new Vector3(4 * j, cone.transform.position.y, i);
                }
            }
            else
            {

                //レーンごとにアイテムを生成
                for (int j = -1; j < 2; j++)
                {
                    //アイテムの種類を決める
                    int item = Random.Range(1, 11);
                    //アイテムを置くZ座標のオフセットをランダムに設定
                    int offsetZ = Random.Range(-5, 6);
                    //60%コイン配置:30%車配置:10%何もなし
                    if (1 <= item && item <= 6)
                    {
                        //コインを生成
                        GameObject coin = Instantiate(coinPrefab) as GameObject;
                        coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, i + offsetZ);
                    }
                    else if (7 <= item && item <= 9)
                    {
                        //車を生成
                        GameObject car = Instantiate(carPrefab) as GameObject;
                        car.transform.position = new Vector3(posRange * j, car.transform.position.y, i + offsetZ);
                    }
                }
            }
        }*/
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

    

    GameObject GenerateStage(int tipIndex)
    {
        int nextStageTip = Random.Range(0, stageTips.Length);
        int posX = Random.Range(-2, 2);
        int rot = 0;
        // 車の場合は回転処理を加える
        if(nextStageTip == 0)
		{
            rot = -90;
		}
        float pos_y = 1;
        
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
        GameObject stageObject = (GameObject)Instantiate(
            stageTips[nextStageTip],
            new Vector3(posX, pos_y, tipIndex * STAGE_TIP_SIZE),
            Quaternion.Euler(rot, 0, 0));
        return stageObject;
    }

    //指定のインデックスまでのステージチップを生成して、管理下におく
    void UpdateStage(int toTipIndex)
    {
        if (toTipIndex <= currentTipIndex) return;

        //指定のステージチップまで生成するよ
        for (int i = currentTipIndex + 1; i <= toTipIndex; i++)
        {
            GameObject stageObject = GenerateStage(i);
            //生成したステージチップを管理リストに追加して、
            generatedStageList.Add(stageObject);
        }
        //ステージ保持上限になるまで古いステージを削除します。
        while (generatedStageList.Count > preInstantiate + 2) DestroyOldestStage();

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
