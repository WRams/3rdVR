using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform PlayerTr;
    public Transform cam;
    Vector3 originPos;
   public bool upView =true;
   public bool forwordView= false;
 
    Transform tr;
    // Update is called once per frame
    private void Update()
    {
        if (forwordView)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
               Camera.main.transform.position = cam.transform.position;

                upView = true;
                forwordView = false;
                return;
            }
        }
        if (upView)
        {
            
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Camera.main.transform.position = PlayerTr.transform.position;
                forwordView = true;
                upView = false;
            }
        }
    }
}
