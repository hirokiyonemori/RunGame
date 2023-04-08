using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
	[SerializeField]
	private List<Button> m_button;
	
	[SerializeField]
	private Button m_optionButton;
	
	[SerializeField]
	private List<GameObject> m_KeyObject;

	public GameObject optionObj;

	private GameManager gameManager;

	[SerializeField]
	private Option m_option;
	
	public GoogleAds googleAds;

	//public GoogleAdMobBanner googleAdMobBanner;


	/// <summary>
	/// 報酬ボタンのコールバック
	/// </summary>
	private void OnRewardButton()
	{
		//googleAds.ShowReawrd();
	}

	void Start()
	{

		for (int i = 0; i  < m_button.Count; i ++)
		{
			int _num = i;
			m_button[i].onClick.AddListener(() =>
		   {
			   
			   onStartButton(_num);   
		   
		   
		   });
			// 一番最初は飛ばす
			if (i == 0)
			{
				m_button[i].interactable = true;
				continue;
			}
			// 0からステージclear判定をいれているので、
			if ( i > 0 && SaveManager.instance.LoadBool("StageClear" + (i - 1 )))
			{
				m_button[i].interactable = true;
			}
			else
			{
				m_button[i].interactable = false;
			}
		}
		m_optionButton.onClick.AddListener(() =>
		{
			optionObj.SetActive(true);
			m_option.Init();
		});
		
		AudioManager.instance.PlayBgm(0);
		
		for (int i = 0; i < m_KeyObject.Count; i++)
		{
			
			if( SaveManager.instance.LoadBool("StageClear" + i ) )
			{
				m_KeyObject[i].SetActive(false);	
			}
		}

		SaveManager.instance.AllLoad();
		//float value = ES3.Load(SaveManager.BGM_VALUE);
		//Debug.Log(" Count = " + LocalizationSettings.AvailableLocales.Locales.Count  );
		
		FadeManager.GetInstance().StartFadeIn();
		m_optionButton.onClick.AddListener( () =>
		{
			optionObj.SetActive(true);
		});
		//googleAdMobBanner.FirstSetting();
		//googleAdMobBanner.RequestBanner(GoogleMobileAds.Api.AdSize.Banner, GoogleMobileAds.Api.AdPosition.Bottom);
		
	}



	public void onStartButton(int num)
	{
		//�@�I�������ԍ�
		//googleAdMobBanner.DestroyBanner();
		PlayerManager.instance.stageNo = num;
		FadeManager.GetInstance().StartFadeOut("Main");

        
	}
	
	
}
