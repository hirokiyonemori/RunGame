using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{

	public List<Button> m_button;

	public Button m_optionButton;

	public GameObject optionObj;

	private GameManager gameManager;


	public GoogleAds googleAds;

	void Start()
	{

		

		for (int i = 0; i  < m_button.Count; i ++)
		{
			int _num = i;
			m_button[i].onClick.AddListener(() =>
		   {
			   onStartButton(_num);
		   });
		}
		m_optionButton.onClick.AddListener(() =>
		{
			optionObj.SetActive(true);
		});
		AudioManager.instance.Setup();
		AudioManager.instance.PlayBgm(0);
	}



	public void onStartButton(int num)
	{
		//Å@ëIëÇµÇΩî‘çÜ
		PlayerManager.instance.stageNo = num;
        SceneManager.LoadScene("Main");
	}
}
