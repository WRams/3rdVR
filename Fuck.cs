using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR;
using UnityEngine.UI;
public class Fuck : MonoBehaviourPun, IPunObservable
{
   public Rigidbody rb;
    
    Vector3 pos;
    Quaternion rot;
    Color col;
    public int ownerViewId;
    public GameObject myCam;
    public GameObject VRCam;
    public GameObject VoiceIcon;
    public GameObject VRUI1;
    public GameObject VRUI2;
    public bool isAlive = true;
    Material mat;
    FuckPlayer fuckPlayer;
    public Text pcSCORE; 
    public Text vrSCORE;

    void Start()
    {
        //print("VRø¨∞·" + isPresent());
        rb = GetComponent<Rigidbody>();
        mat = GetComponent<MeshRenderer>().material;
        mat.color = new Color(Random.value, Random.value, Random.value); 
        
        if (PhotonNetwork.IsMasterClient == false)
        {
            Destroy(GetComponent<ConstantForce>());
            Destroy(rb);
            GetComponent<Collider>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == false)
        {
            transform.position = Vector3.Lerp(transform.position, pos, 0.2f);
            //transform.rotation = Quaternion.Lerp(transform.rotation, rot, 0.2f);
        }
        else 
        {
            if (Netmanager.instance.VRUION||Netmanager.instance.Chat)
        
            {
                VRUI1.SetActive(false);
                VRUI2.SetActive(false);
            }
            else if (Netmanager.instance.VRUION ==false)
            {
                VRUI1.SetActive(true);
                VRUI2.SetActive(true);
            }
   
        }

    }

    public void Move(Vector3 dir, float speed)
    {
        if (rb == null) return;
        dir.y = rb.velocity.y;
        rb.velocity = Vector3.Lerp(rb.velocity, dir, speed * Time.deltaTime);
    }
    public void Rot(float rotX, float rotY)
    {
        //if (rb == null) return;
        
        transform.localEulerAngles = new Vector3(rotX, rotY, 0.0f);

    }
    public void Jump(bool x)
    {
        if (rb == null) return;
        if (x)
        { 
          rb.AddForce(Vector3.up * 15.0f, ForceMode.Impulse);
        }
    }
    public void Stop(bool x ,bool y,bool z)
    {
        if (rb == null) return;
        col = mat.color;
        if (x)
        {       
           GetComponent<ConstantForce>().enabled = false;
            rb.isKinematic = true;
            GetComponent<Collider>().enabled = false;
            mat.color = Color.red;
        }
        if (y)
        {
            GetComponent<ConstantForce>().enabled = true;
            rb.isKinematic = false;
            GetComponent<Collider>().enabled = true;
            GetComponent<MeshRenderer>().enabled = true;
            mat.color = col;
        }
        if (z)
        {
            GetComponent<ConstantForce>().enabled = false;
            rb.isKinematic = true;
            GetComponent<Collider>().enabled = false;
            mat.color = Color.red;
        }

    }
    public void Mass(float mass)
    {
        if (rb == null) return;
        rb.mass=mass;
    }
    public void Voice(bool x, bool y)
    {

        if (x)
        {            
            //if (rb == null) return;
            VoiceIcon.SetActive(true);
            if(ownerViewId == Netmanager.instance.myPhotonView.ViewID)
            {
                print("∏ª«“ ¡ÿ∫Òµ ");
                gameObject.GetComponent<Photon.Voice.Unity.Recorder>().TransmitEnabled = true;
            }

        }
        if (y)
        {
            
            //if (rb == null) return;
            VoiceIcon.SetActive(false);
            if (ownerViewId == Netmanager.instance.myPhotonView.ViewID)
            {
                print("∏ª«“ ¡ÿ∫Ò≤®¡¸");
                gameObject.GetComponent<Photon.Voice.Unity.Recorder>().TransmitEnabled = false;
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            //stream.SendNext(transform.rotation);

        }

        if (stream.IsReading)
        {
            pos = (Vector3)stream.ReceiveNext();
            //rot = (Quaternion)stream.ReceiveNext();

        }
     
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            
            AudioSource ad = gameObject.GetComponentInChildren<AudioSource>();
            ad.Play();
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            GameObject obj = collision.gameObject;
            Rigidbody rd = obj.GetComponent<Rigidbody>();
            Vector3 inNormal = Vector3.Normalize(collision.contacts[0].point - obj.transform.position);
            Vector3 ReflectVector = Vector3.Reflect(collision.relativeVelocity, inNormal);
            rd.AddForce(ReflectVector * (rb.mass * rb.velocity.magnitude * 5f)/(rd.mass* rd.velocity.magnitude), ForceMode.Impulse);
            VRVibe();
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            VRVibe();
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Destroy"))
        {
            VRVibe();
        }
    }
   
    void VRVibe()
    {
        int x = ownerViewId;
        fuckPlayer.photonView.RPC("RpcSendVibeInfo", RpcTarget.All, x);
    }
    
    public void SetOwnerViewID(int viewId, FuckPlayer player)
    {
        fuckPlayer = player;
        photonView.RPC("RpcSetOwnerViewID", RpcTarget.AllBuffered, viewId);
    }
    

    [PunRPC]
    void RpcSendVoiceInfo(bool x, bool y)
    {
        //if (PhotonNetwork.IsMasterClient == false) return;
        Voice(x, y);
    }

    [PunRPC]
    void RpcSetOwnerViewID(int viewId)
    {
        ownerViewId = viewId;
        if (Netmanager.instance.myPhotonView == null) return;
        if(viewId == Netmanager.instance.myPhotonView.ViewID)
        {
            Netmanager.instance.myPhotonView.GetComponent<FuckPlayer>().fuck = this;
            if (Netmanager.instance.isPresent() == true)
            {
                VRCam.SetActive(true);
                return;
            }
            else
            {
                myCam.SetActive(true);
             
            }
            AudioListener.volume = 1;
        }
    }
}
