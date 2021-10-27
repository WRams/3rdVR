using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneLoad : MonoBehaviour
{
    public GameObject VRMap;
    public GameObject PCMap;
    private void Awake()
    {
      
    }
    // Start is called before the first frame update
    void Start()
    {
        if (Netmanager.instance.isPresent() == true)
        {
            VRMap.SetActive(true);
            return;
        }
        else
        {
            PCMap.SetActive(true);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
