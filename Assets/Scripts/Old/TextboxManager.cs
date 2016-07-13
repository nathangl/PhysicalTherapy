using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextboxManager : MonoBehaviour {

	public GameObject textBox;
	public Text mText;
	public int currentLine;
	public int endAtLine;
	public TextAsset textFile;
	public string[] textLines;
	public bool isActive;

	// Use this for initialization
	void Start () {
		if(textFile)
		{
			textLines = (textFile.text.Split('\n'));
		}
		if(endAtLine == 0)
		{
			endAtLine = textLines.Length - 1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(!isActive)
			return;
		mText.text = textLines[currentLine];
		if(Input.GetKeyDown(KeyCode.Return))
		{
			currentLine++;
		}
		if(currentLine > endAtLine) 
		{
			textBox.SetActive(false);
		}
	}

	public void EnableTextBox ()
	{
		textBox.SetActive(true);
	}

	public void DisableTextBox ()
	{
		textBox.SetActive(false);
	}
}
