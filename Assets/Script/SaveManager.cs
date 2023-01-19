using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    int myValue;
    void Start()
    {
        Load();
    }
    void OnApplicationQuit()
    {
        Save();
    }
    void OnApplicationPause(bool paused)
    {
        if (paused)
            Save();
    }
    void Save()
    {
        ES3.Save<int>("Coin", 123);
    }
    void Load()
    {
        if (ES3.KeyExists("Coin"))
		{
            myValue = ES3.Load<int>("Coin");
            
        }
        if (ES3.KeyExists("Tutorial"))
		{
            Debug.Log("Tutorial  ");
		}
		else
		{
            Debug.Log("Tutorial  2");
            ES3.Save<bool>("Tutorial", false);
        }


    }
}
