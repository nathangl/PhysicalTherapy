using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandInteraction : MonoBehaviour {
    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    HashSet<InteractableItem> objectsHoveringOver = new HashSet<InteractableItem>();

    private InteractableItem closestItem;
    private InteractableItem interactingItem;
    private VRanimPath animation;

    // Use this for initialization
    void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

	// Update is called once per frame
  // Checks to see if a controller button is preseed down or up
	void Update () {
        // Checks to see if controller is on and tracking
        if (controller == null)
        {
            Debug.Log("Controller not initialized");
            return;
        }

        // Checks to see if button is pressed down
        if ( controller.GetPressDown(triggerButton))
        {
            float minDistance = float.MaxValue;

            float distance;
            // If the controller is hovering over multiple object
            // this determines which object is closest which is
            // the one that will be selected
            foreach(InteractableItem item in objectsHoveringOver)
            {
                // Checks distance
                distance = (item.transform.position - transform.position).sqrMagnitude;
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestItem = item;
                }
            }

            interactingItem = closestItem;

            if (interactingItem)
            {
                // If the object is already interacting, that interaction
                // is ended
                if (interactingItem.IsInteracting())
                {
                    interactingItem.EndInteraction(this);
                }
                interactingItem.BeginInteraction(this);

            }
        }

        // Checks to see if the user lets go of the button
        if (controller.GetPressUp(triggerButton) && interactingItem != null)
        {
            interactingItem.EndInteraction(this);
        }

    }

    // Detects objects that wand is hovering over
    private void OnTriggerEnter(Collider collider)
    {
        InteractableItem collidedItem = collider.GetComponent<InteractableItem>();
        if (collidedItem)
        {
            objectsHoveringOver.Add(collidedItem);
        }
    }

    // Detects when the controller goes outside of the controller
    private void OnTriggerExit(Collider collider)
    {
        InteractableItem collidedItem = collider.GetComponent<InteractableItem>();
        if (collidedItem)
        {
            objectsHoveringOver.Remove(collidedItem);
        }
    }
}
