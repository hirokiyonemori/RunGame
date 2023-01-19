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
    public void Setup()
    {
        audio = GetComponent<AudioSource>();
        audio.volume = 0.7f;
    }


    AudioSource audio;

    public void PlayBgm(int no)
	{
        audio.clip = clipList[no];
        audio.Play();
    }

    public void PlaySe(int no)
    {
        audio.PlayOneShot(seList[no]);
    }

}
