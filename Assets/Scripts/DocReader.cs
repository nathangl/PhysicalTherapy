#define TESTING
using UnityEngine;
using System;
using System.Collections;
using System.IO;

public class DocReader : MonoBehaviour {

	public TextAsset textFile;// file to be read
	public char input;

	// Use this for initialization
	void Start () {
		#if TESTING
		Debug.Log("Testing");
		#endif
	}
	
	// Update is called once per frame
//	void Update () {
//		try{
//			int.TryParse(input);	
//		}
//		catch {
//			print("please enter a character");
//		}
//	}
}
