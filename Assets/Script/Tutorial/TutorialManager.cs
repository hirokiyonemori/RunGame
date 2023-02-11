using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class TutorialManager : MonoBehaviour
{
    public RectTransform tutorialTextArea;
    public TextMeshProUGUI TutorialTitle;
    public TextMeshProUGUI TutorialText;

    // �`���[�g���A���^�X�N
    protected ITutorialTask currentTask;
    protected List<ITutorialTask> tutorialTask;

    // �`���[�g���A���\���t���O
    //private bool isEnabled;

    // �`���[�g���A���^�X�N�̏����𖞂������ۂ̑J�ڗp�t���O
    private bool task_executed = false;

    // �`���[�g���A���\������UI�ړ�����
    private float fade_pos_x = 460;

    private float fade_pos_y = 90;

    [SerializeField]
    private bool tutorialFlag;

    [SerializeField]
    private GameObject obj;

    void Start()
    {
        // 解像度で移動距離を変更する
        fade_pos_x = 460 * Screen.width / 480;
        fade_pos_y = 90 * Screen.height/ 800;
        
        
        tutorialFlag = ES3.Load<bool>("Tutorial");
        // �`���[�g���A�����I����Ă���ꍇ
        if (tutorialFlag)
        {
            tutorialTextArea.GetComponent<CanvasGroup>().alpha = 0;
            return;
        }


        // �`���[�g���A���̈ꗗ
        tutorialTask = new List<ITutorialTask>()
        {
            new MovementTask(),
            new JumpTask(),
            new EndTask(),
            
        };

        // �ŏ��̃`���[�g���A����ݒ�
        StartCoroutine(SetCurrentTask(tutorialTask.First()));
    }

    void Update()
    {
        // �`���[�g���A�������݂����s����Ă��Ȃ��ꍇ�ɏ���
        if (currentTask != null && !task_executed)
        {
            // ���݂̃`���[�g���A�������s���ꂽ������
            if (currentTask.CheckTask())
            {
                task_executed = true;

                DOVirtual.DelayedCall(currentTask.GetTransitionTime(), () => {
                    iTween.MoveTo(tutorialTextArea.gameObject, iTween.Hash(
                        "position", tutorialTextArea.transform.position + new Vector3(fade_pos_x, 0, 0),
                        "time", 1f
                    ));
                    tutorialTask.RemoveAt(0);

                    var nextTask = tutorialTask.FirstOrDefault();
                    if (nextTask != null)
                    {
                        StartCoroutine(SetCurrentTask(nextTask, 1f));
                    }
                    else
                    {
                        tutorialFlag = true;
                    }
                });
            }
        }

    }

    public bool CheckTutorialEnd()
	{
        return tutorialFlag;
    }

    /// <summary>
    /// �V�����`���[�g���A���^�X�N��ݒ肷��
    /// </summary>
    /// <param name="task"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    protected IEnumerator SetCurrentTask(ITutorialTask task, float time = 0)
    {
        // time���w�肳��Ă���ꍇ�͑ҋ@
        yield return new WaitForSeconds(time);

        currentTask = task;
        task_executed = false;

        // UI�Ƀ^�C�g���Ɛ�������ݒ�
        TutorialTitle.text = task.GetTitle();
        TutorialText.text = task.GetText();

        // �`���[�g���A���^�X�N�ݒ莞�p�̊֐������s
        task.OnTaskSetting();

        iTween.MoveTo(tutorialTextArea.gameObject, iTween.Hash(
            "position", tutorialTextArea.transform.position - new Vector3(fade_pos_x, 0, 0),
            "time", 1f
        ));
    }

    /// <summary>
    /// 特に使っていない
    /// </summary>
    protected void SwitchEnabled()
    {
        //isEnabled = !isEnabled;

        // UI�̕\���؂�ւ�
        //float alpha = isEnabled ? 1f : 0;
        //tutorialTextArea.GetComponent<CanvasGroup>().alpha = alpha;
    }
}
