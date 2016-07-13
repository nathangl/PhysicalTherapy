using UnityEngine;
using System.Collections;

public class MyDialogue : MonoBehaviour
{
	string[] nonPlayerTalking = new string[5];
	string[] playerTalking = new string[5];
	int interactionIndex = 0;
	bool isTalking = true;

	// use this for initialization
	void Start ()
	{
		nonPlayerTalking[0] = "Greeting Friend";
		nonPlayerTalking[1] = "Welcome to this land";
		nonPlayerTalking[2] = "I see you are new here";
		nonPlayerTalking[3] = "I am Gandolf the wizard";
		nonPlayerTalking[4] = "Enjoy your journey";

		playerTalking[0] = "Hello to you too!";
		playerTalking[1] = "Thank you";
		playerTalking[2] = "Yes, I am";
		playerTalking[3] = "Nice to meet you";
		playerTalking[4] = "Thanks and take care";
	}

	void OnGUI ()
	{
		if(isTalking)
		{
			GUI.Label(new Rect(20, 20, 150, 120), nonPlayerTalking[interactionIndex]);
			if(GUI.Button(new Rect(20, 70, 120, 30), playerTalking[interactionIndex]))
			{
				if(interactionIndex >= 4)
				{
					interactionIndex = 4;
				}
				else
				{
					interactionIndex++;
				}
			}
			if(GUI.Button(new Rect(20, 110, 50, 30), "Say Again!"))
			{
				interactionIndex = 0;
			}
			if(GUI.Button(new Rect(20, 150, 150, 30), "Bye."))
			{
				isTalking = false;
			}
		}
	} // OnGUI
} // MyDialogue