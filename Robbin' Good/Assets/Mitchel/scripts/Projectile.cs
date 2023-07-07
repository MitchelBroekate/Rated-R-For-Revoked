using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damg;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<HealthScript>()) 
        {
            collision.transform.GetComponent<HealthScript>().DoDamg(1);
        }
        Destroy(gameObject);
    }
}
