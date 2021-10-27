using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnclickTuto()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void OnclickCaht()
    {
        Netmanager.instance.Chat = true;
        Netmanager.instance.Connect();
    }
}
