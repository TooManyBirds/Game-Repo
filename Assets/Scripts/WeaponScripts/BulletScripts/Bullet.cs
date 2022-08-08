using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected Rigidbody body;
    public float speed = 0.0f;
    [SerializeField]
    protected Vector3 trajectory;
    public float initialLifetime = 1.0f;
    protected float currentLifetime = 0.0f;

    public float startDropoffDistance = 0.0f;
    public float endDropoffDistance = 0.0f;

    public float startDropoffDamage = 0.0f;
    public float endDropoffDamage = 0.0f;

    protected Vector3 initialPosition = new Vector3(0.0f, 0.0f, 0.0f);
    protected Quaternion initialAngle = new Quaternion();


    // Start is called before the first frame update
    protected virtual void Start()
    {
        body = GetComponent<Rigidbody>();
        //Ensures that the arrays for both dropoffDistances and dropoffDamage are of equal length to avoid segmentation fault errors later down the road.
        currentLifetime = initialLifetime;
    }

    public void SetTrajectory(Vector3 direction)
    {
        trajectory = direction;



    }

    public void SetPosition(Vector3 position)
    {
        initialPosition = position;
        gameObject.transform.position = initialPosition;
    }


    public void SetAngle(Quaternion angle)
    {
        initialAngle = angle;
        gameObject.transform.rotation = initialAngle;
    }

    public void SetStartDamage(float damage)
    {
        startDropoffDamage = damage;
    }

    public void SetEndDamage(float damage)
    {
        endDropoffDamage = damage;
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }

    protected virtual float CalculateDamage()
    {
        //Override
        return 0.0f;
    }

    public virtual void Launch()
    {


    }

    private void Update()
    {
        if (currentLifetime <= 0.0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            currentLifetime -= Time.deltaTime;
        }
    }

}
