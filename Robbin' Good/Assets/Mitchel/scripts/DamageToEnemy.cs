using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageToEnemy : MonoBehaviour
{
    RaycastHit gunHit;
    public GameObject gun;
    public Health health;

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out gunHit, 100))
        {
            if (Input.GetMouseButtonDown(0) && gunHit.transform.gameObject.tag == "Enemy" && gun.activeInHierarchy == true)
            {
                health = gunHit.transform.gameObject.GetComponent<Health>();
                DealDamage(1);
            }
        }
    }

    void DealDamage(int damage)
    {
        health.health -= damage;
    }
}
