using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour {
    public Rigidbody rigidBody;

    private bool curentlyInteracting;

    private WandInteraction attachedWand;

    private Transform interactionPoint;

    private float velocityFactor = 20000f;
    private Vector3 posDelta;

    private float rotationFactor = 400f;
    private Quaternion rotationDelta;
    private float angle;
    private Vector3 axis;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        interactionPoint = new GameObject().transform;
        velocityFactor /= rigidBody.mass;
        rotationFactor /= rigidBody.mass;
	}
	
	// Update is called once per frame
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

    public void BeginInteraction(WandInteraction wand)
    {
        
        attachedWand = wand;
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
}
