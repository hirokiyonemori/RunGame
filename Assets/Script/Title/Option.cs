using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class Option : MonoBehaviour
{

    [SerializeField]
    private Slider soundSlider;
    [SerializeField]
    private Slider bgmSlider;

    
    [SerializeField]
    private GameObject optionObj;

    private float beforBgmValue;
    
    private float beforSoundValue;

    private AudioSource audio;
    
    [SerializeField]
    private List<Button> _button;

    private int beforLanNum = 0;
    private int lanNum = 0;
    void Start()
    {
        audio = AudioManager.instance.GetComponent<AudioSource>();
        for (int i = 0; i < _button.Count; i++)
        {
            // ここで格納しないと番号は保存されない
            int _num = i;
            _button[i].onClick.AddListener((() =>
            {
                
                lanNum = _num;
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_num];

            }));
        }
        //throw new NotImplementedException();
    }

    // Start is called before the first frame update
    public void Init()
    {
        beforBgmValue = (float) ES3.Load(SaveManager.BGM_VALUE);
        bgmSlider.value = beforBgmValue;
        beforSoundValue = (float) ES3.Load(SaveManager.SOUND_VALUE);
        soundSlider.value = beforSoundValue;
        beforLanNum = (int) ES3.Load(SaveManager.LANGUAGE);

    }

    // Update is called once per frame
    void Update()
    {
        audio.volume = bgmSlider.value;
         //AudioManager.instance.GetComponent<AudioSource>().volume = soundSlider.value;
    }

    public void OkButton()
    {
        float value = bgmSlider.value;
        
        // BGMが変更する場合
        if (beforBgmValue != bgmSlider.value)
        {
            Debug.Log(" bgmSave ");
            ES3.Save(SaveManager.BGM_VALUE,value);
        }
        else
        {
            Debug.Log(" bgmLoad ");
            value = (float) ES3.Load(SaveManager.BGM_VALUE);
            audio.volume = value;
        }
            
        value = soundSlider.value;
        if (beforSoundValue != soundSlider.value)
        {
            Debug.Log(" SoundSave ");
            ES3.Save(SaveManager.SOUND_VALUE, value);
        }

        if (beforLanNum != lanNum)
        {
            Debug.Log(" SoundSave ");
            ES3.Save<int>(SaveManager.LANGUAGE, lanNum);    
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[lanNum];
        }
        
        optionObj.SetActive(false);
    }
    
    
    public void CanselButton()
    {
        float value = (float) ES3.Load(SaveManager.BGM_VALUE);
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[beforLanNum];
        audio.volume = value;
        optionObj.SetActive(false);
    }

}
