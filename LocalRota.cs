using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalRota : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public bool right;
    // Update is called once per frame
    void Update()
    {
        if(right)
        transform.Rotate(0, -0.1f, 0);
        else
            transform.Rotate(0, 0.1f, 0);
    }
}
