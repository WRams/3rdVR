using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ////if (Netmanager.instance.isPresent() == false)
        ////{
        //    keyboard.SetActive(true);
        //}
        //else
        //{
        //    Controller.SetActive(true);
        //}
    }
    public GameObject keyboard;
    public GameObject Controller;
   
   

    public GameObject pcMove;
    public GameObject pcJump;
    public GameObject pcMass;
    public GameObject pcVoice;
    public GameObject pcView;
    public GameObject vRMove;
    public GameObject vRRot;
    public GameObject vRJump;
    public GameObject vRMass;
    public GameObject vRVoice;
    public GameObject vRView;
 
    // Update is called once per frame
    void Update()
    {
        if (Netmanager.instance.isPresent() == false)
        {
            keyboard.SetActive(true);
            Move();
            Jump();
            Mass();
            Voice();
        }
        else
        {
            Controller.SetActive(true);
            VRMove();
            VRView();
            VRVoice();
            VRRot();
            VRJump();
            VRMass();
           
        }
        
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            pcMove.SetActive(true);
        }
        else
        {
            pcMove.SetActive(false);
        }
    }
    void Jump()
    {
        if (Input.GetKey(KeyCode.X))
        {
            pcJump.SetActive(true);
        }
        else
        {
            pcJump.SetActive(false);
        }
    }
    void Mass()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            pcMass.SetActive(true);
        }
        else
        {
            pcMass.SetActive(false);
        }
    }
    void Voice()
    {
        if (Input.GetKey(KeyCode.P))
        {
            pcVoice.SetActive(true);
        }
        else
        {
            pcVoice.SetActive(false);
        }
    }
    void View()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            pcView.SetActive(true);
        }
        else
        {
            pcView.SetActive(false);
        }
    }
    void VRMove()
    {
        if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch) != new Vector2(0, 0))
        {
            vRMove.SetActive(true);
        }
        else
        {
            vRMove.SetActive(false);
        }
    }
    void VRRot()
    {
        if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch) != new Vector2(0, 0))
        {
            vRRot.SetActive(true);
        }
        else
        {
            vRRot.SetActive(false);
        }
    }
    void VRJump()
    { 
        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.RTouch))
        {
            vRJump.SetActive(true);
        }
        else
        {
            vRJump.SetActive(false);
        }
    }
    void VRView()
    {
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch) > 0)
        {
            vRView.SetActive(true);
            //Transform parent = Controller.transform.parent;
            //Controller.transform.parent = null;
            //Controller.transform.localScale = new Vector3(3f, 3f, 3f);
            //Controller.transform.parent = parent;
        }
        else
        {
            vRView.SetActive(false);
            //Transform parent = keyboard.transform.parent;
            //Controller.transform.parent = null;
            //Controller.transform.localScale = new Vector3(1f, 1f, 1f);
            //Controller.transform.parent = parent;
        }
    }
    void VRMass()
    {
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) > 0)
        {
            vRMass.SetActive(true);
           

        }
        else if(OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) == 0)
        {
            vRMass.SetActive(false);
        
        }
    }
    void VRVoice()
    {
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) > 0)
        {
            vRVoice.SetActive(true);
        }
        else
        {
            vRVoice.SetActive(false);
        }
    }
}
