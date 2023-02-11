using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum StageNo
{
    elementary = 0,
    intermediate= 1,
    senior = 2,
    Menguasai = 3,
}
public class PlayerManager : MonoBehaviour
{

    public static PlayerManager instance;
    public int stageNo = 0;
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
    public void Setup()
    {
        Debug.Log("Setup");
    }

    

}
