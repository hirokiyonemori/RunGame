using System.Collections;
using System.Collections.Generic;
using ES3Internal;
using UnityEngine;

public class Car : MonoBehaviour
{

    [SerializeField]
    private Rigidbody m_Rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(PlayerManager.instance.stageNo >= 3)
        //    this.m_Rigidbody.velocity = new Vector3(0, 0, -10);
    }
}
