using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunScript : MonoBehaviour
{
    public Camera Cam;
    public Transform attackPoint;
    public GameObject bullet;
    public GameObject currentBullet;
    public TextMeshProUGUI ammoDisplay;
    public GameObject muzzleFlash;
    public Rigidbody playerRB;

    public float shootForce;

    public float timeBetweenShooting, reloadTime, timeBetweenShots;
    public int magezineSize, bulletsTap;
    public bool allowButtonHold;

    public float recoilForce;

    int bulletsLeft, bulletsShot;

    bool shooting, readyToShoot, reloading;

    public bool allowInvoke;

    private void Awake()
    {
        bulletsLeft = magezineSize;
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Update()
    {
        MyInput();

        if(ammoDisplay != null)
        {

            ammoDisplay.SetText(bulletsLeft / bulletsTap + "/" + magezineSize / bulletsTap);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(currentBullet.gameObject);
    }

    private void MyInput()
    {
       if(allowButtonHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        } 
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

       if(readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = 0;

            Shoot();
        }

       if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magezineSize && !reloading)
        {
            Reload();
        }

        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0)
        {
            Reload();
        }

    }

    private void Shoot()
    {
        readyToShoot = false;

        Ray ray = Cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75);
        }

        Vector3 DirectionBullet = targetPoint - attackPoint.position;

        currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);

        currentBullet.transform.forward = DirectionBullet.normalized;

        currentBullet.GetComponent<Rigidbody>().AddForce(DirectionBullet.normalized * shootForce, ForceMode.Impulse);

        if(muzzleFlash != null)
        {
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        }

        bulletsLeft--;
        bulletsShot++;

        if (allowInvoke)
        {
            Invoke("ResetShots", timeBetweenShooting);
            allowInvoke = false;

            playerRB.AddForce(-DirectionBullet.normalized * recoilForce, ForceMode.Impulse);
        }

        if(bulletsShot < bulletsTap && bulletsLeft > 0) 
        {
            Invoke("Shoot", timeBetweenShots);
        }
    }

    private void ResetShots()
    {
        readyToShoot = true;
        allowInvoke = true;

    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadDone", reloadTime);
    }

    private void ReloadDone()
    {
        bulletsLeft = magezineSize;
        reloading = false;
    }
}
