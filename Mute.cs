using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mute : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            gameObject.GetComponent<AudioSource>().mute = false;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            gameObject.GetComponent<AudioSource>().mute = true;
        }
    }
}
