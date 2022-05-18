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


    public List<Map> testMapLayout = new List<Map>();


    private void Awake()
    {

    }

    private void createCol(int[] nums)
    {

        
    }    



}


public class Map
{

    public int[] row = new int[5];

    public Map(int[] nums)
    {
        populateRow(nums);
    }

    public void populateRow(int[] nums)
    {
        for(int i = 0; i < row.Length; i++)
        {
            row[i] = nums[i];
        }
    }

    private int[] getRow()
    {
        
        return row;
    }

}