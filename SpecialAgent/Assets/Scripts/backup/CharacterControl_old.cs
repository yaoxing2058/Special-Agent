using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    private float translation;
    private float straffe;
    private CharacterController controller;

    private float yVelocity;
    [SerializeField]
    public float speed = 5.0f;
    [SerializeField]
    private float jumpHeight = 15.0f;
    private float gravity = 1f;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 velocity = new Vector3(Input.GetAxis("Horizontal"), 0, 0) * speed;

        if (controller.isGrounded) {

        if (Input.GetKeyDown(KeyCode.Space)) {
            yVelocity = jumpHeight;
        }

        else {
            yVelocity -= gravity;
        }

        }

        velocity.y = yVelocity;
        controller.Move(velocity * Time.deltaTime);
        
    }
}
