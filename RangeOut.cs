using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeOut : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public Transform death;
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.transform.SetPositionAndRotation(death.position, death.rotation);
            other.gameObject.GetComponent<ConstantForce>().enabled = false;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.GetComponent<Collider>().enabled = false;
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
