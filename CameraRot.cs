using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraRot : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public Transform player;
    float RotSpeed = 200;
    //회전값을 저장하는 
    float rotX = 0;
    float rotY = 0;
    public bool useVertical = false;
    public bool useHorizontal = false;

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == false)
        {
            return;
        }
        gameObject.transform.position = player.transform.position;
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
        rotX = Mathf.Clamp(rotX, -90, 90);
        transform.localEulerAngles = new Vector3(rotX, rotY, 0);
    }
}
