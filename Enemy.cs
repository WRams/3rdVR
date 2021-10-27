using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            GameObject obj = collision.gameObject;
            Rigidbody rd = obj.GetComponent<Rigidbody>();
            Vector3 inNormal = Vector3.Normalize(collision.contacts[0].point - obj.transform.position);
            Vector3 ReflectVector = Vector3.Reflect(collision.relativeVelocity, inNormal);
            rd.AddForce(ReflectVector * rb.mass * 5,
              ForceMode.Impulse);
        }
    }
}
