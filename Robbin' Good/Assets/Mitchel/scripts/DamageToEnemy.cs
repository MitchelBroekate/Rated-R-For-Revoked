using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageToEnemy : MonoBehaviour
{
    RaycastHit gunHit;
    public GameObject gun;
    public int maxAmmo = 8;
    private int currentAmmo;
    public float reloadTime = 2.5f;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (currentAmmo <= 0) 
        {
            StartCoroutine(Reload());
            return;
        }

        if (Physics.Raycast(transform.position, transform.forward, out gunHit, 100))
        {
            if (Input.GetMouseButtonDown(0))
            {
                currentAmmo --;
                if (gunHit.transform.gameObject.tag == "Enemy" && gun.activeInHierarchy == true)
                {
                    Health health = gunHit.transform.gameObject.GetComponent<Health>();
                    health.DealDamage(20);
                }
            }
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
    }
}
