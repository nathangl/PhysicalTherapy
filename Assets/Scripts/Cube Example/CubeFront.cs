using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class CubeFront : MonoBehaviour
{
    void OnMouseDown()
    {
        CubeTree.DetermineClick("Front");
    }
}
