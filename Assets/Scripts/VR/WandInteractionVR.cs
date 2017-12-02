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
  void Update () {
    if (controller == null) {
    Debug.Log("Controller not initialized");
    return;
    }

    if ( controller.GetPressDown(triggerButton)) {
      if (interactingItem) {
        if (interactingItem.IsInteracting()) {
          interactingItem.EndInteraction(this);
        }
        interactingItem.BeginInteraction(this);
      }
      else if(interactingUI){
        Debug.Log("Button pressed");
        if(interactingUI.IsInteracting()){
          interactingUI.EndInteraction(this);
        }
        interactingUI.BeginInteraction(this);
      }
    }

    if (controller.GetPressUp(triggerButton)) {
      if (interactingItem) {
        interactingItem.EndInteraction(this);
        //interactingItem = null;
      }
      if (interactingUI) {
        interactingUI.EndInteraction(this);
      }
    }

  }

  private void OnTriggerEnter(Collider collider) {
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

  private void OnTriggerExit(Collider collider) {
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
