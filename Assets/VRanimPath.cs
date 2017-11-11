using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class VRanimPath : MonoBehaviour
{
    

    private bool curentlyInteracting;

	private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }

	private SteamVR_TrackedObject trackedObj;

    private Transform interactionPoint;

    //private float velocityFactor = 20000f;
	public Vector3 posDelta;
	//public Vector3 wandPos;
    //private float rotationFactor = 400f;
    private Quaternion rotationDelta;
    private float angle;
    private Vector3 axis;

    //Animation path variables
    Vector3 player;
    float minDistance = float.PositiveInfinity;
    float minPercent = 0;
    Vector3 pos;
    public Animator anim;
    bool active = false;
    public animPathsManager manager;
    float position; //the position in animation or time in animation


    // Use this for initialization
    void Start()
    {
        iTween.PutOnPath(gameObject, iTweenPath.GetPath(gameObject.name), 0);
		trackedObj = GetComponent<SteamVR_TrackedObject>();
        //interactionPoint = new GameObject().transform;
        //velocityFactor /= rigidBody.mass;
        //rotationFactor /= rigidBody.mass;
    }

    // Update is called once per frame
    void Update()
    {
		if (controller.GetPressDown(triggerButton) && posDelta != null)
        {
            /*posDelta = attachedWand.transform.position - interactionPoint.position;
            this.rigidBody.velocity = posDelta * velocityFactor * Time.fixedDeltaTime;

            rotationDelta = attachedWand.transform.rotation * Quaternion.Inverse(interactionPoint.rotation);
            rotationDelta.ToAngleAxis(out angle, out axis);

            if (angle > 180)
            {
                angle -= 360;
            }

            this.rigidBody.angularVelocity = (Time.fixedDeltaTime * angle * axis) * rotationFactor;*/
			

            if (gameObject.name == manager.prevAnim || manager.first == true)
            {

                //posDelta = wandPos.transform.position;
                Vector3 objPosition = Camera.main.ScreenToWorldPoint(posDelta);

                float pos = DeterminePos(objPosition);
                gameObject.transform.position = objPosition;

                iTween.PutOnPath(gameObject, iTweenPath.GetPath(gameObject.name), pos);

                position = (DeterminePos(objPosition)) / 2;
                anim.Play(gameObject.name, 0, position);
                //Debug.Log(position);
                
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

    public void BeginInteraction(WandInteraction wand)
    {

        //attachedWand = wand;
        interactionPoint.position = wand.transform.position;
        interactionPoint.rotation = wand.transform.rotation;

        interactionPoint.SetParent(transform, true);
        //this.transform.SetParent(wand.transform);
        curentlyInteracting = true;

    }

    public void EndInteraction(WandInteraction wand)
    {
        if (wand == attachedWand)
        {
            attachedWand = null;
            curentlyInteracting = false;
            //this.transform.SetParent(null);
        }
    }

    public bool IsInteracting()
    {
        return curentlyInteracting;
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
