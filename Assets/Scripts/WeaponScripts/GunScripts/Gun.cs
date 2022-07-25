using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Bullet bullet;
    public float ammoCapacity = 0.0f;
    float currentAmmoAmount = 0;
    public float damageOverride = 0.0f;
    public float speedOverride = 0.0f;

    public float reloadCooldown = 0.0f;
    [SerializeField]
    float currentReloadCooldown = 0.0f;
    [SerializeField]
    bool bIsReloading = false;

    public float maxPerBulletCooldown = 0.0f;
    float currentPerBulletCooldown = 0.0f;

    public GameObject barrelEnd;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmoAmount = ammoCapacity;
    }

    public virtual void Shoot()
    {
        if (currentAmmoAmount > 0 && currentPerBulletCooldown <= 0.0f)
        {
            if (bullet)
            {
                Bullet newBullet = Instantiate(bullet);
                if (newBullet)
                {
                    currentAmmoAmount -= 1.0f;
                    newBullet.SetPosition(barrelEnd.gameObject.transform.position);
                    newBullet.SetTrajectory(barrelEnd.gameObject.transform.rotation.eulerAngles);
                    currentPerBulletCooldown = maxPerBulletCooldown;
                   // newBullet.Launch();
                }
                else
                {
                    Debug.LogError("BULLET NOT CREATED.");
                }
            }      
            else
            {
                Debug.LogError("BULLET DOES NOT EXIST");
            }
        }
    }

    public virtual void Reload()
    {
        if (currentAmmoAmount < ammoCapacity)
        {
            bIsReloading = true;
            currentReloadCooldown = reloadCooldown;

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (currentReloadCooldown > 0.0f && bIsReloading)
        {
            currentReloadCooldown -= Time.deltaTime;
        }
        else if (currentReloadCooldown <= 0.0f && bIsReloading)
        {
            bIsReloading = false;
        }
        if (currentPerBulletCooldown > 0.0f && !bIsReloading)
        {
            currentPerBulletCooldown -= Time.deltaTime;
        }
        else if (currentReloadCooldown <= 0.0f && bIsReloading)
        {
            currentAmmoAmount = ammoCapacity;
            currentReloadCooldown = reloadCooldown;
            bIsReloading = false;
        }
    }
}
