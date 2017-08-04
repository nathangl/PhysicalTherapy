using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipHover : MonoBehaviour {

    public GameObject text;
    //public int offsetX;
    //public int offsetY;

	// Use this for initialization
	void Start () {
        text.gameObject.SetActive(false);
	}
	
	public void PointerEnter()
    {
        text.gameObject.SetActive(true);
        //text.transform.position = Input.mousePosition + new Vector3(offsetX, offsetY, 0);
    }

    public void Cancel()
    {
        text.gameObject.SetActive(false);
    }
}
