using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SumoPlayerGun : MonoBehaviour
{
    [SerializeField]Rigidbody playerRb;
    [SerializeField] float recoilForce;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootPoint;
    [SerializeField] float shootForce;
    //Gun stats
    public float timeBetweenShooting, spreadNormal,  timeBetweenShots;
    float spread;
    public int  bulletsPerTap;
    public bool allowButtonHold;

    int  bulletsShot;

    static bool shooting;
    bool readyToShoot;

    public ParticleSystem muzzleFlash;

    public bool allowInvoke = true;
    [SerializeField] FixedJoystick stick;
    void Start()
    {
        readyToShoot = true;
    }

    void Update()
    {
        MyInput();
    }
    private void MyInput()
    {
        if(stick.Horizontal != 0 || stick.Vertical != 0)
        {
            shooting = true;
        }
        else
        {
            shooting = false;
        }

        if (readyToShoot && shooting)
        {
            bulletsShot = 0;
            Shoot();
        }
            spread = spreadNormal / 10;
    }

    void Shoot()
    {
        readyToShoot = false;
        Vector3 directionWithoutSpread = -playerRb.transform.right;
        float x = Random.Range(-spread / 10, spread / 10);
        float y = Random.Range(-spread / 10, spread / 10);
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);
        if(bullet != null)
        {
            GameObject currentBullet = Instantiate(bullet, shootPoint.position, playerRb.transform.rotation);

            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        }
        playerRb.AddForce(-directionWithSpread.normalized * recoilForce, ForceMode.Impulse);
        muzzleFlash.Play();
        bulletsShot++;
        if (allowInvoke)
        {
            playerRb.AddForce(-directionWithSpread.normalized * recoilForce, ForceMode.Impulse);
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }
        if (bulletsShot < bulletsPerTap)
        {
            Invoke("Shoot", timeBetweenShots);
        }

    }
    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }
}
