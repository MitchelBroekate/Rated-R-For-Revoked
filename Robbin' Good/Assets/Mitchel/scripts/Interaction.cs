using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intercation : MonoBehaviour
{
    RaycastHit hit;

    void Start()
    {
        
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, 1000))
        {

        }
    }
}
