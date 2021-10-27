using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class VRCameraMove : MonoBehaviour
{
    public Transform PlayerTr;
    public Transform cam;
    Vector3 originPos;
   public bool upView =true;
   public bool forwordView= false;
    private void Awake()
    {
       
        
        //originRot = gameObject.transform.rotation;
    }
    void Start()
    {
    
    }
    Transform tr;
    // Update is called once per frame
    private void Update()
    {
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch) > 0)
        {
            print(OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch));
            gameObject.transform.position = cam.transform.position;

        }
       if (Input.GetKeyDown(KeyCode.Tab) || OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch)==0)
            {
                gameObject.transform.position =  PlayerTr.transform.position;
                             
                
            }
       
    }
}
