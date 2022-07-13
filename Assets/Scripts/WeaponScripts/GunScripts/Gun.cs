using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public float ammoCapacity = 0.0f;
    float currentAmmoAmount = 0;
    public float damageOverride = 0.0f;
    public float speedOverride = 0.0f;

    public float reloadCooldown = 0.0f;
    float currentReloadCooldown = 0.0f;
    bool bIsReloading = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public virtual void Shoot()
    {
        if (currentAmmoAmount > 0)
        {
            GameObject newBullet = Instantiate(bullet);
            if (newBullet)
            {
                currentAmmoAmount -= 1.0f;
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
            currentAmmoAmount = ammoCapacity;
            currentReloadCooldown = reloadCooldown;
            bIsReloading = false;
        }
    }
}
