using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToEnemy : MonoBehaviour
{
    RaycastHit gunHit;
    public GameObject gun;


    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out gunHit, 1000))
        {
            if (Input.GetMouseButtonDown(0) && gun.activeInHierarchy == true && gunHit.transform.TryGetComponent<Health>)
            {
                TakeDamage(20);
            }
        }
    }
}
