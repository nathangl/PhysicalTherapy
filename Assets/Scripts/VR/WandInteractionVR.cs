using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandInteractionVR : MonoBehaviour {
  private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
  private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

  private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
  private SteamVR_TrackedObject trackedObj;

  private VRanimPath interactingItem;
  private InteractableUI interactingUI;

// Use this for initialization
  void Start () {
    trackedObj = GetComponent<SteamVR_TrackedObject>();
  }

  // Update is called once per frame
  // Checks to see if a controller button is preseed down or up
  void Update () {
    // Checks to see if controller is on and tracking
    if (controller == null) {
    Debug.Log("Controller not initialized");
    return;
    }

    // Checks to see if button is pressed down
    if ( controller.GetPressDown(triggerButton)) {
      // Checks to see if it is an object or a button
      if (interactingItem) {
        // If the object is already interacting, that interaction
        // is ended
        if (interactingItem.IsInteracting()) {
          interactingItem.EndInteraction(this);
        }
        interactingItem.BeginInteraction(this);
      }
      else if(interactingUI){
        //Debug.Log("Button pressed");
        // If the object is already interacting, that interaction
        // is ended
        if(interactingUI.IsInteracting()){
          interactingUI.EndInteraction(this);
        }
        interactingUI.BeginInteraction(this);
      }
    }

    // Checks to see if the user lets go of the button
    if (controller.GetPressUp(triggerButton)) {
      // Checks to see if it is an object or a button
      if (interactingItem) {
        interactingItem.EndInteraction(this);
        //interactingItem = null;
      }
      if (interactingUI) {
        interactingUI.EndInteraction(this);
      }
    }

  }

  // Detects objects that wand is hovering over
  private void OnTriggerEnter(Collider collider) {
    // Checks to see if it is an object or a button
    if (collider.GetComponent<VRanimPath>()){
      VRanimPath collidedItem = collider.GetComponent<VRanimPath>();
      if (collidedItem) {
        interactingItem = collidedItem;
      }
    }
    else if(collider.GetComponent<InteractableUI>()){
      InteractableUI collidedUI = collider.GetComponent<InteractableUI>();
      if(collidedUI){
        interactingUI = collidedUI;
      }
    }
  //InteractableItem collidedItem = collider.GetComponent<InteractableItem>();
    Debug.Log (collider);
  }

  // Detects when the controller goes outside of the controller
  private void OnTriggerExit(Collider collider) {
    // Checks to see if it is an object or a button
    if (interactingItem) {
      interactingItem.EndInteraction(this);
      interactingItem = null;
    }
    if (interactingUI) {
      interactingUI.EndInteraction(this);
      interactingUI = null;
    }
  }
}
