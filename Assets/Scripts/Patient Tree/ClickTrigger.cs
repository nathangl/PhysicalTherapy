using UnityEngine;
using System.Collections;

public class ClickTrigger : MonoBehaviour {

    public string treeInput;

    void OnMouseDown()
    {
        PatientTree.DetermineClick(treeInput);
    }
}
