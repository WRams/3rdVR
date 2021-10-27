using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    GameObject target;
    // Update is called once per frame
    void Update()
    {
        
    }
    Vector3 direct;
    private void OnTriggerEnter(Collider other)
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            GameObject obj = collision.gameObject;
            Rigidbody rd = obj.GetComponent<Rigidbody>();
            //rb.isKinematic = true;
            Vector3 inNormal = Vector3.Normalize(collision.contacts[0].point - obj.transform.position);
            Vector3 ReflectVector = Vector3.Reflect(collision.relativeVelocity, inNormal);
            print(collision.relativeVelocity);
            rd.AddForce(ReflectVector*10*rd.mass, ForceMode.Impulse);

            Vib(0.5f);

        }
    }
    private void OnCollisionExit(Collision collision)
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        //rb.isKinematic = false;
    }
    IEnumerator Vib(float sec)
    { //진동신호 주기
        OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
        OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
        yield return new WaitForSeconds(sec);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
    }
}
