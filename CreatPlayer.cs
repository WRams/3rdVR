using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.XR;

public class CreatPlayer : MonoBehaviourPun
{
    public static CreatPlayer instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        if (photonView == null)
        {
            PhotonNetwork.Instantiate("EmptyPlayer", new Vector3(0, 0.5f, 0), Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
