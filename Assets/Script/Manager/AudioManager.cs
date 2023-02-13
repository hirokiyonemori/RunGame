using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static AudioManager instance;

    [SerializeField]
    private AudioSource audio;

    public List<AudioClip> clipList;

    public List<AudioClip> seList;

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

    private float volume;
    public void Setup(float volme)
    {
        audio.volume = volme;
    }


    public void loop(bool loop)
    {
        audio.loop = loop;
    }

    public void PlayBgm(int no)
	{
        audio.clip = clipList[no];
        audio.Play();
    }

    public void StopBgm(int no)
    {
        //audio.clip = clipList[no];
        audio.Stop();
    }

    public void PlaySe(int no)
    {
        float value = (float)ES3.Load(SaveManager.SOUND_VALUE);
        audio.PlayOneShot(seList[no],value);
    }

}
