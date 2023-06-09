using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunScript : MonoBehaviour
{
    [Header("Links")]
    public Camera Cam;
    public Transform attackPoint;
    public GameObject bullet;
    public GameObject currentBullet;
    public GameObject active;
    public Rigidbody playerRB;
    public Animator animator;

    [Header("UI")]
    public TextMeshProUGUI ammoDisplay;

    [Header("Effect")]
    public GameObject muzzleFlash;

    [Header("Audio")]
    public AudioSource source;
    public AudioClip gunShotClip;
    public AudioClip reloadClip;

    [Header("Gun settings")]

    public float shootForce;

    public float timeBetweenShooting, reloadTime, timeBetweenShots;
    public int magezineSize, bulletsTap;
    public bool allowButtonHold;

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
            if (reloading)
            {
                ammoDisplay.SetText("Reloading...");
            }
            else
            {
                ammoDisplay.SetText(bulletsLeft / bulletsTap + "/" + magezineSize / bulletsTap);
            }
        }
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

       if(readyToShoot && shooting && !reloading && bulletsLeft > 0 && active.activeInHierarchy == true)
        {
            bulletsShot = 0;

            Shoot();
        }

       if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magezineSize && !reloading && active.activeInHierarchy == true)
        {
            Reload();
        }

        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0 && active.activeInHierarchy == true)
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
        if (!reloading)
        {
            animator.SetBool("Shooting", true);
        } else
        {
            animator.SetBool("Shooting", false);
        }

        source.PlayOneShot(gunShotClip);

        if (allowInvoke)
        {
            Invoke("ResetShots", timeBetweenShooting);
            allowInvoke = false;
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
        animator.SetBool("Shooting", false);

    }

    private void Reload()
    {
        reloading = true;
        animator.SetBool("Reload", true);
        Invoke("ReloadDone", reloadTime);

        source.PlayOneShot(reloadClip);
    }

    private void ReloadDone()
    {
        bulletsLeft = magezineSize;
        reloading = false;
        animator.SetBool("Reload", false);
    }
}
