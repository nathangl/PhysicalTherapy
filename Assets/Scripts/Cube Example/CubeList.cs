using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cube
{
    public string side { get; set; }
    public string display { set; get; }
}


public class CubeList : MonoBehaviour
{

    static List<Cube> cubeList = new List<Cube>()
        {
         new Cube { side= "top", display = "TOP CLICKED"},
         new Cube { side = "front", display = "FRONT CLICKED"},
        };

    static public void DetermineClick(string side1)
    {
        Cube cube1 =
            cubeList.Find(cuber => cuber.side == side1);
        Debug.Log(cube1.display);
    }

}