using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Destroy : MonoBehaviourPun
{
    AudioSource ad;
    Fuck Fuck;
    public Transform death;
    void Start()
    {
        ad = gameObject.GetComponent<AudioSource>();
    }
  
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //ParticleSystem ps = other.gameObject.GetComponent<ParticleSystem>();
        //ps.transform.position = other.transform.position;
        //ps.Play();
        //ad.Play();
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.GetComponent<Fuck>().isAlive = false;
            other.gameObject.transform.SetPositionAndRotation(death.position,death.rotation);
            other.gameObject.GetComponent<ConstantForce>().enabled = false;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.GetComponent<Collider>().enabled = false;
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
