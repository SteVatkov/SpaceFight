using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class PilotMovement : MonoBehaviour
{

    public float verticalInputAcceleration = 1;
    public float horizontalInputAcceleration = 20;

    public float maxSpeed = 10;
    public float maxRotationSpeed = 100;
    public float maxBoostRotationSpeed = 20;
    public float brakeSpeed = 10;

    public float velocityDrag = 1;
    public float rotationDrag = 1;

    [SerializeField] public Vector3 velocity;
    private float zRotationVelocity;

    Vector3 startPosition;

    private bool accelerating = false;
    public bool isSwinging = false;
    public bool wasSwinging = false;

    public Vector2 ropeHook;
    public float swingForce = 4f;

    public Animator thrusterAnimator;

    public UnityEvent thrustersOn;
    public UnityEvent thrustersOff;

    private Rigidbody2D rBody;

    Vector2 perpendicularDirection;

    private void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

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


        if (isSwinging)
        {
            float horizontalInput = Input.GetAxis("Horizontal");

            // 1 - Get a normalized direction vector from the player to the hook point
            var playerToHookDirection = (ropeHook - (Vector2)transform.position).normalized;

            // 2 - Inverse the direction to get a perpendicular direction
            
            if (horizontalInput < 0)
            {
                perpendicularDirection = new Vector2(-playerToHookDirection.y, playerToHookDirection.x);
                var leftPerpPos = (Vector2)transform.position - perpendicularDirection * -2f;
                Debug.DrawLine(transform.position, leftPerpPos, Color.green, 0f);
            }
            else
            {
                perpendicularDirection = new Vector2(playerToHookDirection.y, -playerToHookDirection.x);
                var rightPerpPos = (Vector2)transform.position + perpendicularDirection * 2f;
                Debug.DrawLine(transform.position, rightPerpPos, Color.green, 0f);
            }

            wasSwinging = true;
        }
        else if (!isSwinging && wasSwinging)
        {
            var force = perpendicularDirection * swingForce;
            rBody.AddForce(force, ForceMode2D.Force);
            wasSwinging = false;
        }
    }
    
}
