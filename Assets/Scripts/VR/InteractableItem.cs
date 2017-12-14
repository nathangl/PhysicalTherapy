using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour {
    public Rigidbody rigidBody;

    private bool curentlyInteracting;

    private WandInteraction attachedWand;

    private Transform interactionPoint;

    private float velocityFactor = 20000f; //Multiplier for determining displacement needed to move object
    private Vector3 posDelta;

    private float rotationFactor = 400f; //Multiplier for determining displacement needed to move object
    private Quaternion rotationDelta;
    private float angle;
    private Vector3 axis;

	// Initializes rigidBody to the objects rigidbody
  // the point at which the controller is touching the objects
  // Takes the factors and divides them by the objects mass
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        interactionPoint = new GameObject().transform;
        velocityFactor /= rigidBody.mass;
        rotationFactor /= rigidBody.mass;
	}

	// Update is called once per frame
  // Checks to see if the controller is interacting with the object
  // Sets the velocity of an object to follow the controller
	void Update () {
		if (attachedWand && curentlyInteracting)
        {
            posDelta = attachedWand.transform.position - interactionPoint.position;
            this.rigidBody.velocity = posDelta * velocityFactor * Time.fixedDeltaTime;

            rotationDelta = attachedWand.transform.rotation * Quaternion.Inverse(interactionPoint.rotation);
            rotationDelta.ToAngleAxis(out angle, out axis);

            if (angle > 180)
            {
                angle -= 360;
            }

            this.rigidBody.angularVelocity = (Time.fixedDeltaTime * angle * axis) * rotationFactor;
        }
	}
    // Called from WandInteraction
    // Sets the interactionPoint to the Wands position
    // Sets currentlyInteracting to True
    public void BeginInteraction(WandInteraction wand)
    {

        attachedWand = wand;
        interactionPoint.position = wand.transform.position;
        interactionPoint.rotation = wand.transform.rotation;

        interactionPoint.SetParent(transform, true);
        //this.transform.SetParent(wand.transform);
        curentlyInteracting = true;

    }
    // Called from WandInteraction
    // Sets attachedWand to null
    public void EndInteraction(WandInteraction wand)
    {
        if (wand == attachedWand) // Checks which wand is ending the interaction
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
}
