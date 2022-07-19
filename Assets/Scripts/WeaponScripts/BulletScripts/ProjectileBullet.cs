using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum DamageCalculationType : int
{
    OverTime,
    OverDistance
}

public class ProjectileBullet : Bullet
{

    public DamageCalculationType type = DamageCalculationType.OverDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override float CalculateDamage()
    {
        //base.CalculateDamage();
        if (type == DamageCalculationType.OverDistance)
        {
            Vector3 endPosition = body.position;
            Vector3 finalPosition = endPosition - initialPosition;
            if (finalPosition.magnitude < startDropoffDistance)
            {
                return startDropoffDamage;
            }
            else if (finalPosition.magnitude < endDropoffDistance)
            {
                return startDropoffDamage - Mathf.Lerp(startDropoffDamage, endDropoffDamage, (finalPosition.magnitude - startDropoffDistance) / (endDropoffDistance - startDropoffDistance));
            }
            else
            {
                return endDropoffDamage;
            }
        }
        else
        {
            //TEMP. May need

            if (currentLifetime < startDropoffDistance)
            {
                return startDropoffDamage;
            }
            else if (currentLifetime < endDropoffDistance)
            {
                return startDropoffDamage - Mathf.Lerp(startDropoffDamage, endDropoffDamage, (currentLifetime - startDropoffDistance) / (endDropoffDistance - startDropoffDistance));
            }
            else
            {
                return endDropoffDamage;
            }
        }
        
    }

    private void OnEnable()
    {
        body.AddForce(trajectory * speed);
    }
}
