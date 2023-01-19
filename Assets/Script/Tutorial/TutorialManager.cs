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

    // チュートリアルタスク
    protected ITutorialTask currentTask;
    protected List<ITutorialTask> tutorialTask;

    // チュートリアル表示フラグ
    private bool isEnabled;

    // チュートリアルタスクの条件を満たした際の遷移用フラグ
    private bool task_executed = false;

    // チュートリアル表示時のUI移動距離
    private float fade_pos_x = 460;


    [SerializeField]
    private bool tutorialFlag;

    void Start()
    {
        // チュートリアル表示用UIのインスタンス取得
        //tutorialTextArea = GameObject.Find("TutorialTextArea").GetComponent<RectTransform>();
        //TutorialTitle = tutorialTextArea.Find("Title").GetComponent<TextMeshPro>();
        //TutorialText = tutorialTextArea.Find("Text").GetComponentInChildren<TextMeshPro>();
        
        tutorialFlag = ES3.Load<bool>("Tutorial");
        // チュートリアルが終わっている場合
        if (tutorialFlag) return;


        // チュートリアルの一覧
        tutorialTask = new List<ITutorialTask>()
        {
            new MovementTask(),
            new JumpTask(),
            new EndTask(),
            //new AttackTask2(),
        };

        // 最初のチュートリアルを設定
        StartCoroutine(SetCurrentTask(tutorialTask.First()));

        isEnabled = true;

        SwitchEnabled();
    }

    void Update()
    {
        // チュートリアルが存在し実行されていない場合に処理
        if (currentTask != null && !task_executed)
        {
            // 現在のチュートリアルが実行されたか判定
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
    /// 新しいチュートリアルタスクを設定する
    /// </summary>
    /// <param name="task"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    protected IEnumerator SetCurrentTask(ITutorialTask task, float time = 0)
    {
        // timeが指定されている場合は待機
        yield return new WaitForSeconds(time);

        currentTask = task;
        task_executed = false;

        // UIにタイトルと説明文を設定
        TutorialTitle.text = task.GetTitle();
        TutorialText.text = task.GetText();

        // チュートリアルタスク設定時用の関数を実行
        task.OnTaskSetting();

        iTween.MoveTo(tutorialTextArea.gameObject, iTween.Hash(
            "position", tutorialTextArea.transform.position - new Vector3(fade_pos_x, 0, 0),
            "time", 1f
        ));
    }

    /// <summary>
    /// チュートリアルの有効・無効の切り替え
    /// </summary>
    protected void SwitchEnabled()
    {
        isEnabled = !isEnabled;

        // UIの表示切り替え
        //float alpha = isEnabled ? 1f : 0;
        //tutorialTextArea.GetComponent<CanvasGroup>().alpha = alpha;
    }
}
