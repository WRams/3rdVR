using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class Netmanager : MonoBehaviourPunCallbacks
{

    public static Netmanager instance;
    int yCnt;
    public PhotonView myPhotonView;
    public bool Chat;
    public bool VRUION;
    public bool start = false;
    public GameObject[] fucks;
    List<GameObject> Players = new List<GameObject>();
    int count;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }
    void Start()
    {


    }
    public bool isPresent()
    {
        var xrDisplaySubsytems = new List<XRDisplaySubsystem>();
        SubsystemManager.GetInstances(xrDisplaySubsytems);
        foreach (var xrDisplay in xrDisplaySubsytems)
        {
            if (xrDisplay.running)
            {
                return true;
            }
        }
        return false;
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(60);
        start = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient == false) return;
        if (Input.GetKeyDown(KeyCode.Y) || OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            yCnt++;
            if (yCnt == 1)
                PhotonNetwork.LoadLevel("WHG1");
            if (yCnt == 2)
                PhotonNetwork.LoadLevel("WHG2");
            if (yCnt == 3)
                PhotonNetwork.LoadLevel("WHG3");
            if (yCnt == 4)
                PhotonNetwork.LoadLevel("Deserts");
            if (yCnt == 5)
                PhotonNetwork.LoadLevel("NoGravity");

        }

        fucks = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < fucks.Length; i++)
        {
            if (fucks[i].GetComponent<Fuck>().isAlive)
            {
                count++;
                if(isPresent() == true)
                    fucks[i].GetComponent<Fuck>().vrSCORE.text = "Alive Player : " + count;
                else
                fucks[i].GetComponent<Fuck>().pcSCORE.text = "Alive Player : " + count;
               
            }
        }

        if (start == true)
        {
            if (count <= 1)
            {
                int a = SceneManager.sceneCount;

                if (a == 1)
                {
                    print(a);
                    PhotonNetwork.LoadLevel("WHG1");
                }
                else if (a == 2)
                    PhotonNetwork.LoadLevel("WHG2");
                else if (a == 3)
                    PhotonNetwork.LoadLevel("WHG3");
                else if (a == 4)
                    PhotonNetwork.LoadLevel("Deserts");
                else if (a == 5)
                    PhotonNetwork.LoadLevel("NoGravity");
            }
        }
        count = 0;

    }
    public void Connect()
    {
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 60;
        PhotonNetwork.AutomaticallySyncScene = true;

        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.NickName = "Player" + Random.Range(0, 1000);
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();


        print("로비입장");
        if (Chat)
        {
            PhotonNetwork.JoinOrCreateRoom("ChatRoom", new RoomOptions(), TypedLobby.Default);
        }
        else
            PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    }
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();

        print("방생성");
    }
    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient == false) return;
        base.OnJoinedRoom();
        if (Chat)
        {
            PhotonNetwork.LoadLevel("ChatRoom");
        }
        else
        {
            StartCoroutine(Wait());
            
            PhotonNetwork.LoadLevel("TopBlade");
        }

    }
    public void OntoggleUI()
    {
        VRUION = true;
    }
    public void ONClickchatOn()
    {
        Chat = true;
    }


}
