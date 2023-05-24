using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAndHealth : MonoBehaviour
{
    RaycastHit gunHit;

    [Header("Health")]
    public float healthAmount = 100f;

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out gunHit, 1000))
        {
            if (Input.GetMouseButtonDown(0))
            {
                TakeDamage(20);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;

        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
    }
}
