using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoGravity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Transform death;
    private void OnTriggerEnter(Collider other)
    {
        //ParticleSystem ps = other.gameObject.GetComponent<ParticleSystem>();
        //ps.transform.position = other.transform.position;
        //ps.Play();
        //ad.Play();
       
        other.gameObject.GetComponent<Rigidbody>().useGravity = false;
        other.gameObject.GetComponent<Rigidbody>().mass = 0.001f;
        other.gameObject.GetComponent<ConstantForce>().enabled = false;
 
      
    }
    private void OnTriggerExit(Collider other)
    { 
        other.gameObject.transform.SetPositionAndRotation(death.position, death.rotation);
        other.gameObject.GetComponent<ConstantForce>().enabled = false;
        other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        other.gameObject.GetComponent<Collider>().enabled = false;
        other.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
}
