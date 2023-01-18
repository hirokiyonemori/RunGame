using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCreate : MonoBehaviour
{
    //carPrefab������
    public GameObject carPrefab;
    //coinPrefab������
    public GameObject coinPrefab;
    //cornPrefab������
    public GameObject conePrefab;
    //�X�^�[�g�n�_
    private int startPos = -160;
    //�S�[���n�_
    private int goalPos = 120;

    //�X�e�[�W�`�b�v�̔z��
    public GameObject[] stageTips;

    //int�^��ϐ�StageTipSize�Ő錾���܂��B
    const int STAGE_TIP_SIZE = 32;

    //�A�C�e�����o��x�����͈̔�
    private float posRange = 3.4f;
    //������X�e�[�W���폜
    public List<GameObject> generatedStageList = new List<GameObject>();

    //�^�[�Q�b�g�L�����N�^�[�̎w�肪�o����l�ɂ����
    public Transform character;

    //�����������鎞�Ɏg���ϐ�startTipIndex
    public int startTipIndex;
    //�X�e�[�W�����̐�ǂ݌�
    public int preInstantiate;

    const int CAR = 0;
    const int COIN = 1;
    const int CONE = 2;

    //int�^��ϐ�currentTipIndex�Ő錾���܂��B
    int currentTipIndex;



    // Use this for initialization
    void Start()
    {
        /*//���̋������ƂɃA�C�e���𐶐�
        for (int i = startPos; i < goalPos; i += 15)
        {
            //�ǂ̃A�C�e�����o���̂��������_���ɐݒ�
            int num = Random.Range(0, 10);
            if (num <= 1)
            {
                //�R�[����x�������Ɉ꒼���ɐ���
                for (float j = -1; j <= 1; j += 0.4f)
                {
                    GameObject cone = Instantiate(conePrefab) as GameObject;
                    cone.transform.position = new Vector3(4 * j, cone.transform.position.y, i);
                }
            }
            else
            {

                //���[�����ƂɃA�C�e���𐶐�
                for (int j = -1; j < 2; j++)
                {
                    //�A�C�e���̎�ނ����߂�
                    int item = Random.Range(1, 11);
                    //�A�C�e����u��Z���W�̃I�t�Z�b�g�������_���ɐݒ�
                    int offsetZ = Random.Range(-5, 6);
                    //60%�R�C���z�u:30%�Ԕz�u:10%�����Ȃ�
                    if (1 <= item && item <= 6)
                    {
                        //�R�C���𐶐�
                        GameObject coin = Instantiate(coinPrefab) as GameObject;
                        coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, i + offsetZ);
                    }
                    else if (7 <= item && item <= 9)
                    {
                        //�Ԃ𐶐�
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


        //�L�����N�^�[�̈ʒu���猻�݂̃X�e�[�W�`�b�v�̃C���f�b�N�X���v�Z���܂�
        int charaPositionIndex = (int)(character.position.z / STAGE_TIP_SIZE);
        //���̃X�e�[�W�`�b�v�ɓ�������X�e�[�W�̍X�V�������s���܂��B
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
        // �Ԃ̏ꍇ�͉�]������������
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

    //�w��̃C���f�b�N�X�܂ł̃X�e�[�W�`�b�v�𐶐����āA�Ǘ����ɂ���
    void UpdateStage(int toTipIndex)
    {
        if (toTipIndex <= currentTipIndex) return;

        //�w��̃X�e�[�W�`�b�v�܂Ő��������
        for (int i = currentTipIndex + 1; i <= toTipIndex; i++)
        {
            GameObject stageObject = GenerateStage(i);
            //���������X�e�[�W�`�b�v���Ǘ����X�g�ɒǉ����āA
            generatedStageList.Add(stageObject);
        }
        //�X�e�[�W�ێ�����ɂȂ�܂ŌÂ��X�e�[�W���폜���܂��B
        while (generatedStageList.Count > preInstantiate + 2) DestroyOldestStage();

        currentTipIndex = toTipIndex;

    }

    //��ԌÂ��X�e�[�W���폜���܂�
    void DestroyOldestStage()
    {
        GameObject oldStage = generatedStageList[0];
        generatedStageList.RemoveAt(0);
        Destroy(oldStage);
    }
}
