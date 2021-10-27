using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatRull : MonoBehaviour
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
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<ConstantForce>().enabled = false;

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.transform.SetPositionAndRotation(death.position, death.rotation);
        }
    }
}
