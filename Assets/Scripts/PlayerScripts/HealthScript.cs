using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    private int maxhp = 10;
    public int hp;

    private void Start()
    {
        hp = maxhp;
    }

    private void Update()
    {

    }

    public void ReceiveDamage(float damage)
    {
        hp -= (int)damage;
        Debug.Log("Take Damage");
    }
}
