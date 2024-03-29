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

    protected override void Start()
    {
        base.Start();
        Launch();
    }


    public override void Launch()
    {
        base.Launch();
       // body.AddForce(trajectory * speed);
        body.AddRelativeForce(trajectory * speed);
      //  gameObject.transform.rotation = 


    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
