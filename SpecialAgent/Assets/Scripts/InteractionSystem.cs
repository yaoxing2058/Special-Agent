using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionSystem : MonoBehaviour
{
    public float rayRange = 4;

    // Check if interaction is entered
    public bool isPressed;

    // Check if an activatable object is activated
   // public bool isActivated;

    [Header("Interaction Message")]
    public GameObject textBox;

    [Header("Pickup Message")]
    public GameObject pickupMsg;

    [Header("Activation Message")]
    public GameObject activateMsg;


    // Start is called before the first frame update
    void Start()
    {
        textBox.SetActive(false);
        pickupMsg.SetActive(false);
        activateMsg.SetActive(false);
        isPressed = false;
    }

    void CastRay() {
        
     

            RaycastHit hitInfo = new RaycastHit();

            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, rayRange);

            if (hit && !Input.GetMouseButton(1)) {

                GameObject hitObject = hitInfo.transform.gameObject;

                // Interact
                if (hitObject.tag == "Interactable") {

                    if (!isPressed) {

                        textBox.SetActive(true);
                
                        if (Input.GetKeyDown(KeyCode.E)) {
                            hitObject.GetComponent<IInteractable>().Interact();
                            textBox.SetActive(false);
                            isPressed = !isPressed;
                            Time.timeScale = 0;
                        }
                    }

                    else if (isPressed && Input.GetKeyDown(KeyCode.E)) {
                        hitObject.GetComponent<IInteractable>().Cancel();
                        isPressed = !isPressed;
                        textBox.SetActive(true);
                        Time.timeScale = 1;
                    }
                }

                // Pickup
                if (hitObject.tag == "Pickable") {

                    pickupMsg.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.E)) {
                        pickupMsg.SetActive(false);
                        hitObject.GetComponent<IPickable>().Pickup();
                    }
                }

                // Activation
                if (hitObject.tag == "Activatable") {

                    activateMsg.GetComponent<TextMeshProUGUI>().SetText("Press E to " + hitObject.GetComponent<IActivatable>().GetAction());
                    activateMsg.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.E)) {

                        hitObject.GetComponent<IActivatable>().Activate();
                        hideActivateMessage(hitObject.GetComponent<IActivatable>().GetActivateObject());

                    }
                   
                }
            }

            else {
                textBox.SetActive(false);
                pickupMsg.SetActive(false);
                activateMsg.SetActive(false);
            }

    }

    // Hide Activate Messgae When activating or deactivating
    void hideActivateMessage(GameObject activateObject) {

         while (AnimatorIsPlaying(activateObject.GetComponent<Animator>(), "Activate") || 
            AnimatorIsPlaying(activateObject.GetComponent<Animator>(), "Deactivate")) {
                activateMsg.SetActive(false);
            }

    }

    // Return true/false whether specific state is playing in specific animator
    // Get the animator from weapon gameobject tagged as "WeaponName Functions" as the first argument
    // Check this animator for specific state in the animator controller
    private bool AnimatorIsPlaying(Animator anim, string stateName) {
        return anim.GetCurrentAnimatorStateInfo(0).length > anim.GetCurrentAnimatorStateInfo(0).normalizedTime 
        && anim.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    // Update is called once per frame
    void Update()
    {
        CastRay();
    }
}
