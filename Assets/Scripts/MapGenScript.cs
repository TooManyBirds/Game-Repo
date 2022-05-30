using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenScript : MonoBehaviour
{
    public GameObject[] surfaces;
    
    public int[] column1 = new int[5];
    protected int[] column2 = new int[5];
    protected int[] column3 = new int[5];
    protected int[] column4 = new int[5];
    protected int[] column5 = new int[5];

    // Error CS0236 Initalizing variables in a bad scope?
    //public Map Map1 = new Map(column1);


    private void Awake()
    {

    }

    private void createCol(int[] nums)
    {

        
    }    



}