using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("Sway Settings")]
    [SerializeField] private float smooth;
    [SerializeField] private float swayMultiplier;

    // Idle Sway Animation
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    void KeyboardSway() {
         // get mouse input
        if ((Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f) && !Input.GetKey(KeyCode.LeftShift)) {
        anim.enabled = false;
        float mouseX = Input.GetAxisRaw("Horizontal") * swayMultiplier;
        float mouseY = Input.GetAxisRaw("Vertical") * swayMultiplier;

        // calculate target rotation
        Quaternion rotationX = Quaternion.AngleAxis(mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        // rotate 
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
        }
    }

    void IdleSway() {
        
        if (Input.GetAxisRaw("Mouse X") == 0f && Input.GetAxisRaw("Mouse Y") == 0f 
        && Input.GetAxisRaw("Horizontal") == 0f && Input.GetAxisRaw("Vertical") == 0f && !Input.GetKey(KeyCode.Mouse1)) {
            anim.enabled = true;
            anim.SetTrigger("Idle");
        }
        else {
            anim.SetTrigger("Active");
        }
    }

    void MovementSway() {
       if ((Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f) && Input.GetKey(KeyCode.LeftShift)) {
            anim.enabled = true;
            anim.SetTrigger("Run");
       }
       else {
            anim.SetTrigger("Not Run");
       }
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetAxisRaw("Mouse X") != 0f || Input.GetAxisRaw("Mouse Y") != 0f ) {
            
            anim.enabled = false;

            // get mouse input
            float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier;
            float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;

            // calculate target rotation
            Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
            Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

            Quaternion targetRotation = rotationX * rotationY;

            // rotate 
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
        }  
    }

    void Update() {
        KeyboardSway();
        MovementSway();
        IdleSway();
    }
}
