using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class CubeTop : MonoBehaviour {

    void OnMouseDown()
    {
        CubeTree.DetermineClick("Top");
    }
}
