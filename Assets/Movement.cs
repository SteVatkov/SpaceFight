using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class Movement : MonoBehaviour
{

    public float verticalInputAcceleration = 1;
    public float horizontalInputAcceleration = 20;

    public float maxSpeed = 10;
    public float maxRotationSpeed = 100;
    public float maxBoostRotationSpeed = 20;
    public float brakeSpeed = 10;

    public float velocityDrag = 1;
    public float rotationDrag = 1;

    [SerializeField] private Vector3 velocity;
    private float zRotationVelocity;

    Vector3 startPosition;

    private bool accelerating = false;

    public Animator thrusterAnimator;

    public UnityEvent thrustersOn;
    public UnityEvent thrustersOff;

    private void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        
        // apply forward input
        Vector3 acceleration = verticalInput * verticalInputAcceleration * transform.up;

        if (verticalInput > 0.01)
        {
            velocity += acceleration * Time.deltaTime;

            if (!accelerating)
            {
                accelerating = true;
                thrustersOn.Invoke();
            }
            
        }
        else if (verticalInput < -0.01)
        {
            velocity -= (velocity / maxSpeed) * Time.deltaTime;
            if (accelerating)
            {
                accelerating = false;
                thrustersOff.Invoke();
            }
        }
        else
        {
            if (accelerating)
            {
                accelerating = false;
                thrustersOff.Invoke();
            }
        }

        // apply turn input
        float zTurnAcceleration = -1 * Input.GetAxis("Horizontal") * horizontalInputAcceleration;
        zRotationVelocity += zTurnAcceleration * Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.R)) { transform.position = startPosition; }

        thrusterAnimator.SetFloat("Thrust", Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        // apply velocity drag
        velocity = velocity * (1 - Time.deltaTime * velocityDrag);

        // clamp to maxSpeed
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        // apply rotation drag
        zRotationVelocity = zRotationVelocity * (1 - Time.deltaTime * rotationDrag);

        // clamp to maxRotationSpeed
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.01) { zRotationVelocity = Mathf.Clamp(zRotationVelocity, -maxBoostRotationSpeed, maxBoostRotationSpeed); }
        else zRotationVelocity = Mathf.Clamp(zRotationVelocity, -maxRotationSpeed, maxRotationSpeed);

        // update transform
        transform.position += velocity * Time.deltaTime;
        transform.Rotate(0, 0, zRotationVelocity * Time.deltaTime);
    }
    
}
