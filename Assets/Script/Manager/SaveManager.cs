using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class SaveManager : MonoBehaviour
{

    public const string COIN = "Coin";
    public const string STAGE_CLEAR = "StageClear";
    public const string SOUND_VALUE = "SoundValue";
    public const string BGM_VALUE = "BGMValue";
    public const string LANGUAGE = "Language";
    public const string LIFE = "Life";
    
    int myValue;
    public static SaveManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        AllLoad();
    }
    void OnApplicationQuit()
    {
        AllSave();
    }
    void OnApplicationPause(bool paused)
    {
        if (paused)
            AllSave();
    }
    void AllSave()
    {
        ES3.Save<int>("Coin", 123);
    }
    public void AllLoad()
    {
        
        
        if (ES3.KeyExists(COIN))
		{
            myValue = ES3.Load<int>(COIN);
            
        }
        if (ES3.KeyExists("Tutorial"))
		{
            Debug.Log("Tutorial  ");
		}
		else
		{
            ES3.Save<bool>("Tutorial", false);

            Debug.Log("Tutorial  1");
        }
        if (ES3.KeyExists(SOUND_VALUE))
        {
            
            Debug.Log(SOUND_VALUE + ES3.Load(SOUND_VALUE));
            float value = (float)ES3.Load(SOUND_VALUE);
            //AudioManager.instance.Setup(value);
        }
        else
        {
            
            ES3.Save(SOUND_VALUE,1f);
        }
        if (ES3.KeyExists(LIFE))
        {
            Debug.Log(LIFE + ES3.Load(LIFE));
            ///int value = ES3.Load(LIFE);
            //AudioManager.instance.Setup(value); ;
        }
        else
        {
            ES3.Save(LIFE,3);
            Debug.Log(LIFE + ES3.Load(LIFE));
        }
        
        if (ES3.KeyExists(BGM_VALUE))
        {
            Debug.Log(BGM_VALUE + ES3.Load(BGM_VALUE));
            float value = (float) ES3.Load(BGM_VALUE);
            AudioManager.instance.Setup(value); ;
        }
        else
        {
            
            ES3.Save(BGM_VALUE, 0.7f);
            AudioManager.instance.Setup(0.7f); 
        }
        if (ES3.KeyExists(LANGUAGE))
        {
            Debug.Log("LANGUAGE2");
            myValue = ES3.Load<int>(LANGUAGE);
            Debug.Log("myValue" + myValue);
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[myValue];
        }
        else
        {
            Debug.Log("LANGUAGE");
            ES3.Save<int>(LANGUAGE, 0);
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
        }
    }

    public void SaveInt(string str,int value)
    {
        ES3.Save<int>(str, value);
    }
    
    public void Save(string str,AnyState value)
    {
        ES3.Save(str, value);
    }

    public void SaveBool(string str,bool value)
    {
        ES3.Save<bool>(str, value);
    }
    
    public bool Load(string str)
    {
        return ES3.KeyExists(str);
    }
    public bool LoadBool(string str)
    {
        if (ES3.KeyExists(str))
        {
            return ES3.Load<bool>(str);
        }
        else
        {
            ES3.Save<bool>(str, false);
        }
        return false;
    }
    
    public int Loadint(string str)
    {
        if (ES3.KeyExists(str))
        {
            return ES3.Load<int>(str);
        }
        else
        {
            ES3.Save<int>(str, -1);
        }
        return -1;
    }
    
    
}
