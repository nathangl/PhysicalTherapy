using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableUI : MonoBehaviour {
  public Rigidbody rigidBody;

  private WandInteractionVR attachedWand;

  private Transform interactionPoint;

  private bool currentlyInteracting;
  public GameObject PROM;
  // Use this for initialization
  void Start () {
    //this.SetActive(false);
  }

  // Update is called once per frame
  void Update () {
    if (attachedWand && currentlyInteracting) {

    }
  }

  public void BeginInteraction(WandInteractionVR wand) {
    currentlyInteracting = true;
    PROM.gameObject.SetActive(true); // Enables iTween path

  }

  // Currently not working but is not necessary
  public void EndInteraction(WandInteractionVR wand) {
    if (wand == attachedWand) {
      attachedWand = null;
      currentlyInteracting = false;
      PROM.gameObject.SetActive(false); // Disables iTween path
      //this.transform.SetParent(null);
    }
  }

  public bool IsInteracting() {
    return currentlyInteracting;
  }
}
