using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRanimPath : MonoBehaviour {

	//FOR VR
	private bool currentlyInteracting;
	private WandInteractionVR attachedWand;
	private Transform interactionPoint;
	private float velocityFactor = 20000f;
	private Vector3 posDelta;

	//FOR ITWEEN
	Vector3 player;
	float minDistance = float.PositiveInfinity;
	float minPercent = 0;
	Vector3 pos;
	public Animator anim;
	bool active = false;
	public animPathsManager manager;
	float position;		//the position in animation or time in animation

	//Text test
	public Text ouchTxt;

	// Use this for initialization
	void Start () {
		iTween.PutOnPath(gameObject, iTweenPath.GetPath(gameObject.name), 0);
		interactionPoint = new GameObject().transform;

	}

	// Update is called once per frame
	void Update () {
		if (attachedWand && currentlyInteracting)
		{
			if (gameObject.name == manager.prevAnim || manager.first == true)
			{
				//Use interactionPoint instead?
				//Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
//				Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

				BeginInteraction (attachedWand);
				float pos = DeterminePos(interactionPoint.position);
				gameObject.transform.position = interactionPoint.position;

				iTween.PutOnPath(gameObject, iTweenPath.GetPath(gameObject.name), pos);

				//If raised over 90%, ouch text displays
				if (pos > .9f)
					ouchTxt.gameObject.SetActive (true);
				else if(pos < .9f)
					ouchTxt.gameObject.SetActive(false);

				position = (DeterminePos(interactionPoint.position)) / 2;
				anim.Play(gameObject.name, 0, position);


				manager.prevPos = position;
				manager.prevAnim = gameObject.name;
				manager.prevObj = gameObject;
				manager.first = false;
			}
			else
			{
				iTween.PutOnPath(manager.prevObj, iTweenPath.GetPath(manager.prevAnim), 0);
				manager.prevAnim = gameObject.name;
			}
		}
	}

	public void BeginInteraction(WandInteractionVR wand)
	{
		attachedWand = wand;
		interactionPoint.position = wand.transform.position;
		interactionPoint.rotation = wand.transform.rotation;

		//interactionPoint.SetParent(transform, true);
		//this.transform.SetParent(wand.transform);
		currentlyInteracting = true;

	}

	public void EndInteraction(WandInteractionVR wand)
	{
		if (wand == attachedWand)
		{
			attachedWand = null;
			currentlyInteracting = false;
			//this.transform.SetParent(null);
		}
		Debug.Log ("End Interaction");
	}

	public bool IsInteracting()
	{
		return currentlyInteracting;
	}

	float DeterminePos(Vector3 input)
	{
		float minDistance = float.PositiveInfinity;
		float minPercent = 0;

		for (float t = 0; t <= 1; t += 0.005f)
		{
			//float dist = ((Vector2)input - (Vector2)iTween.PointOnPath(iTweenPath.GetPath("PROMRightArm"), t)).sqrMagnitude;
			float dist = ((Vector2)input - (Vector2)iTween.PointOnPath(iTweenPath.GetPath(gameObject.name), t)).sqrMagnitude;

			if (dist < minDistance)
			{
				minDistance = dist;
				minPercent = t;
				pos = iTween.PointOnPath(iTweenPath.GetPath(gameObject.name), t);
			}
		}
		//Debug.Log(minPercent);
		return minPercent;
	}
}
