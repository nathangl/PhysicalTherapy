using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class CubeSide : MonoBehaviour
{
    void OnMouseDown()
    {
        CubeTree.DetermineClick("Side");
    }
}
