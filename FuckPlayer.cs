using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;



public class FuckPlayer : MonoBehaviourPun
{
    //PhotonView photonView;
    public bool isGround;
    Transform tr;
    Rigidbody rb;
    GameObject prCam;
    public GameObject deathCam;
    float speed = 1;
    float maxSpeed = 10;
    float currTime;
    float accel = 0.6f;
    float mass = 1;
    float RotSpeed = 200;
    float VRRotSpeed = 150;
    float rotX = 0;
    float rotY = 0;
    public bool useVertical = false;
    public bool useHorizontal = false;
    public Fuck fuck;

    private void Awake()
    {
       
    }
    void Start()
    {
        
        if (photonView.IsMine)
        {
            Netmanager.instance.myPhotonView = photonView;
        }

        if (PhotonNetwork.IsMasterClient)
        {   
            fuck = PhotonNetwork.Instantiate("Cube", Vector3.zero, Quaternion.identity).GetComponent<Fuck>();
            fuck.SetOwnerViewID(photonView.ViewID, this);
            
        }
    }
   

    // Update is called once per frame
    void Update()
    {
        if (Netmanager.instance.isPresent() == false)
        {
            PMove();
            PRot();
            PJump();
            PStop();
            PWeightIncreace();
            PVoice();
        }
        else
        {
            VRMove();
            VRRot();
            VRJump();
            VRStop();
            VRWeightIncreace();
            VRVoice();
        }

        //if (prCam == null)
        //{
        //    deathCam.SetActive(true);
        //}
        //else
        //{
        //    deathCam.SetActive(false);
        //}


    }

    [PunRPC]
    void RpcSendMoveInfo(Vector3 dir, float speed)
    {
        if (PhotonNetwork.IsMasterClient == false) return;
        fuck.Move(dir, speed);
    }
    [PunRPC]
    void RpcSendRotInfo(float rotX, float rotY)
    {
        if (PhotonNetwork.IsMasterClient == false) return;
        fuck.Rot(rotX, rotY);
    }
    [PunRPC]
    void RpcSendJumpInfo(bool x)
    {
        if (PhotonNetwork.IsMasterClient == false) return;
        fuck.Jump(x);
    }
    [PunRPC]
    void RpcSendStopInfo(bool x,bool y,bool z)
    {
        if (PhotonNetwork.IsMasterClient == false) return;
        fuck.Stop(x,y,z);
    }
    [PunRPC]
    void RpcSendMassInfo(float mass)
    {
        if (PhotonNetwork.IsMasterClient == false) return;
        fuck.Mass(mass);
    }
    
    [PunRPC]
    void RpcSendVibeInfo(int x)
    {
        if(photonView.IsMine)
        {
            StartCoroutine(Vib(0.5f));
        }
        //OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
        //OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
    }
    void PMove()
    {
        if (photonView.IsMine == false)
        {
            return;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)
            /*||OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick,OVRInput.Controller.LTouch)!=new Vector2(0,0)*/)
        {
            if (speed < maxSpeed)
                speed += accel * Time.deltaTime;
        }
        else
        {
            speed -= 2f * Time.deltaTime;
            if (speed <= 1)
                speed = 1;
        }
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //Vector2 s = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);
     
        Vector3 dir = new Vector3(h * speed, 0, v* speed);
        if(Camera.main != null)
        {
            dir = Camera.main.transform.TransformDirection(dir);
            photonView.RPC("RpcSendMoveInfo", RpcTarget.All, dir, speed);
        }

    }
    void VRMove()
    {
        

        if (photonView.IsMine == false)
        {
            return;
        }
        if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick,OVRInput.Controller.LTouch)!=new Vector2(0,0))
        {
            if (speed < maxSpeed)
                speed += accel * Time.deltaTime;
        }
        else
        {
            speed -= 2f * Time.deltaTime;
            if (speed <= 1)
                speed = 1;
        }
        
        Vector2 s = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);

        Vector3 dir = new Vector3(s.x * speed, 0,s.y * speed);
        if (Camera.main != null)
        {
            dir = Camera.main.transform.TransformDirection(dir);
            photonView.RPC("RpcSendMoveInfo", RpcTarget.All, dir, speed);
        }

    }
    void PRot()
    {
        if (photonView.IsMine == false)
        {
            return;
        }

        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        if (useVertical == true)
        {
            rotX += -my * RotSpeed * Time.deltaTime;
        }

        if (useHorizontal == true)
        {
            rotY += mx * RotSpeed * Time.deltaTime; 
        }
    
        rotX = Mathf.Clamp(rotX, -90.0f, 90.0f);
        fuck.Rot(rotX, rotY);
        //photonView.RPC("RpcSendRotInfo", RpcTarget.All, rotX, rotY);
    }
    void VRRot()
    {
       
        if (photonView.IsMine == false)
        {
            return;
        }
        Vector2 s = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);
        print(OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch));

        if (useVertical == true)
        {
            rotX += -s.y * VRRotSpeed * Time.deltaTime;
        }

        if (useHorizontal == true)
        {
            rotY += s.x * VRRotSpeed * Time.deltaTime;
        }
        print(rotX + rotY);
        rotX = Mathf.Clamp(rotX, 0.0f, 0.0f);
        fuck.Rot(rotX, rotY);
        //photonView.RPC("RpcSendRotInfo", RpcTarget.All, rotX, rotY);
    }
    void PJump()
    {
        if (photonView.IsMine == false)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            photonView.RPC("RpcSendJumpInfo", RpcTarget.All, Input.GetKeyDown(KeyCode.X));
        }
    }
    void VRJump()
    {
        
        if (photonView.IsMine == false)
        {
            return;
        }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.RTouch))
        {
            photonView.RPC("RpcSendJumpInfo", RpcTarget.All, OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.RTouch));
        }
    }
    void PStop()
    {
        if (photonView.IsMine == false)
        {
            return;
        }
        if (Input.GetKey(KeyCode.Alpha9)|| Input.GetKeyUp(KeyCode.Alpha9)|| Input.GetKeyDown(KeyCode.Alpha8))
        {
            photonView.RPC("RpcSendStopInfo", RpcTarget.All, Input.GetKey(KeyCode.Alpha9),Input.GetKeyUp(KeyCode.Alpha9), Input.GetKeyDown(KeyCode.Alpha8));
        }
       
    }
    void VRStop()
    {
      
        if (photonView.IsMine == false)
        {
            return;
        }
        if (OVRInput.Get(OVRInput.Button.Two, OVRInput.Controller.LTouch)|| OVRInput.GetUp(OVRInput.Button.Two, OVRInput.Controller.LTouch)|| OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.LTouch))
        {
            photonView.RPC("RpcSendStopInfo", RpcTarget.All, OVRInput.Get(OVRInput.Button.Two, OVRInput.Controller.LTouch), OVRInput.GetUp(OVRInput.Button.Two, OVRInput.Controller.LTouch), OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.LTouch));
        }
    }
    void PVoice()
    {
        if (photonView.IsMine == false)
        {
            return;
        }
        if (Input.GetKey(KeyCode.P) || Input.GetKeyUp(KeyCode.P))
        {
            fuck.photonView.RPC("RpcSendVoiceInfo", RpcTarget.All, Input.GetKey(KeyCode.P), Input.GetKeyUp(KeyCode.P));
        }

    }
    void VRVoice()
    {
        if (photonView.IsMine == false)
        {
            return;
        }
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) > 0 ||
            OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) == 0)
        {
            fuck.photonView.RPC("RpcSendVoiceInfo", RpcTarget.All, OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) > 0,
                OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) == 0);
        }
    }
    void PWeightIncreace()
    {
        if (photonView.IsMine == false)
        {
            return;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            currTime += Time.deltaTime;
            mass = 10.0f;               
            
            if (currTime > 3.0f)
              mass = 1.0f;
        }
        //print(mass);
        if (Input.GetKeyUp(KeyCode.Space))
        {
            mass = 1.0f;
            currTime = 0.0f;
        }
        //print(mass+"무게초기화");
        photonView.RPC("RpcSendMassInfo", RpcTarget.All, mass);
    }
    void VRWeightIncreace()
    {
        
        if (photonView.IsMine == false)
        {
            return;
        }
     
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch)>0)
        {

            currTime += Time.deltaTime;
            mass = 10.0f;

            if (currTime > 3.0f)
                mass = 1.0f;
        }
        //print(mass);
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch)==0)
        {
            mass = 1.0f;
            currTime = 0.0f;
        }
        //print(mass+"무게초기화");
        photonView.RPC("RpcSendMassInfo", RpcTarget.All, mass);
    }
    public void VRVibe(int x)

    {
        if (x == photonView.ViewID)
        {
            Vib(0.5f);
        }

    }
    IEnumerator Vib(float sec)
    { //진동신호 주기
        OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
        OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
        yield return new WaitForSeconds(sec);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
    }
    //void CamVeiw()
    //{
    //    if (photonView.IsMine == false) return;
    //    if (forwordView)
    //    {
    //        if (Input.GetKeyDown(KeyCode.Tab))
    //        {
    //            Camera.main.transform.position = cam.transform.position;

    //            upView = true;
    //            forwordView = false;
    //            return;
    //        }
    //    }
    //    if (upView)
    //    {
    //        if (Input.GetKeyDown(KeyCode.Tab))
    //        {
    //            Camera.main.transform.position = PlayerTr.transform.position;
    //            forwordView = true;
    //            upView = false;
    //        }
    //    }
    //}
}
