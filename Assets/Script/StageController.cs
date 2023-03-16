using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageController : MonoBehaviour
{
    //int�^��ϐ�StageTipSize�Ő錾���܂��B
    const int STAGE_TIP_SIZE = 32;
    //int�^��ϐ�currentTipIndex�Ő錾���܂��B
    int currentTipIndex;
    //�^�[�Q�b�g�L�����N�^�[�̎w�肪�o����l�ɂ����
    public Transform character;
    //�X�e�[�W�`�b�v�̔z��
    public GameObject[] stageTips;
    //�����������鎞�Ɏg���ϐ�startTipIndex
    public int startTipIndex;
    //�X�e�[�W�����̐�ǂ݌�
    public int preInstantiate;
    //������X�e�[�W�`�b�v�̕ێ����X�g
    public List<GameObject> generatedStageList = new List<GameObject>();

    // ゴールまでのチップ数
    public int runDistance;

    public GameObject goalPrefab;

    private bool goalFlag;

    [SerializeField]
    private TextMeshProUGUI distanceCompanyText;

    //　ゴールまでの距離を格納
    private int goalDisCom;

    void Start()
    {
        //����������
        currentTipIndex = startTipIndex - 1;
        //UpdateStage(preInstantiate);
        goalFlag = false;
        runDistance = 10 + 10 * PlayerManager.instance.stageNo;
        StageNo stageNo = (StageNo)PlayerManager.instance.stageNo;
        switch (stageNo)
        {
            case StageNo.elementary:
                runDistance = 10;
                break;
            case StageNo.intermediate:
                runDistance = 10 + 10;
                break;
            case StageNo.senior:
                runDistance = 10 + 100;
                break;
            case StageNo.Menguasai:
                runDistance = 10 + 10000;
                break;
        }
        //goalDisCom = (runDistance + preInstantiate * STAGE_TIP_SIZE -7 );
        // 15はゴールオブジェクトのオフセット
        goalDisCom = runDistance  * STAGE_TIP_SIZE + 15;
    }


    void Update()
    {
        int charaPositionIndex = (int)(character.position.z / STAGE_TIP_SIZE);
        int dis = goalDisCom - (int)(character.localPosition.z);
        if (dis <= 0) dis = 0;
        distanceCompanyText.text = ""+(dis);
        
        // ゴールまできたら表示しない
        if (goalFlag)
		{
            return;
		}
        //���̃X�e�[�W�`�b�v�ɓ�������X�e�[�W�̍X�V�������s���܂��B
        if (charaPositionIndex + preInstantiate > currentTipIndex)
        {
            UpdateStage(charaPositionIndex + preInstantiate);
        }
    }
    
    void UpdateStage(int toTipIndex)
    {
        if (toTipIndex <= currentTipIndex) return;
        
        for (int i = currentTipIndex + 1; i <= toTipIndex; i++)
        {
            GameObject stageObject = GenerateStage(i);
            
            generatedStageList.Add(stageObject);
        }
        //Debug.Log(" generatedStageList " + generatedStageList.Count );
        while (generatedStageList.Count > preInstantiate + 2 ) DestroyOldestStage();

        currentTipIndex = toTipIndex;
    }
    
    
    GameObject GenerateStage(int tipIndex)
    {
        int nextStageTip = Random.Range(0, stageTips.Length);
        //Debug.Log(" tipIndex " + tipIndex );
        if (tipIndex >= runDistance)
        {
            GameObject obj = (GameObject)Instantiate(
                goalPrefab,
                new Vector3(0, 0, tipIndex * STAGE_TIP_SIZE),
                Quaternion.Euler(0, 90, 0));
        
            goalFlag = true;
            PlayerManager.instance.lastFlag = true;
            return obj;
        }
        GameObject stageObject = (GameObject)Instantiate(
            stageTips[nextStageTip],
            new Vector3(0, 0, tipIndex * STAGE_TIP_SIZE),
            Quaternion.Euler(0,90,0));
        return stageObject;
    }
    //��ԌÂ��X�e�[�W���폜���܂�
    void DestroyOldestStage()
    {
        GameObject oldStage = generatedStageList[0];
        generatedStageList.RemoveAt(0);
        Destroy(oldStage);
    }
}
