using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    //physics engine codes!

    public float gravity;
    public Vector2 velocity;
    public float jumpVelocity = 20;
    public float maxVelocity = 100;

    public float maxAcceleration = 10;
    public float acceleration = 10; // To adjust(increasejj)speed(v)

    public float distance = 0;

    public float groundHeight=10;
    public bool isGrounded = false;

    public bool isHoldingJump = false; // higher to higher
    public float maxHoldJumpTime = 0.8f; //don't jump so much
    public float holdJumpTimer = 0.0f;


    public float jumpGroundThreshold = 1; // close enough to the ground



    void Start()
    {
        
    }

    
    void Update()
    {
        Vector2 position = transform.position;
        float groundDistance =  Mathf.Abs(position.y - groundHeight);
        if (isGrounded || groundDistance <= jumpGroundThreshold) //close enough to the ground,All right, we can jump again
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGrounded = false;
                velocity.y = jumpVelocity;
                isHoldingJump=true;
                holdJumpTimer = 0.0f;
            }
        }
        
        if(Input.GetKeyUp(KeyCode.Space))
        {
            isHoldingJump = false;
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = transform.position;
        
        if(!isGrounded)
        {

            if(isHoldingJump)
            {
                holdJumpTimer += Time.fixedDeltaTime;
                if(holdJumpTimer >= maxHoldJumpTime)
                {
                    isHoldingJump = false;
                }
            }

            position.y += velocity.y * Time.fixedDeltaTime;
            if (!isHoldingJump)
            {
                velocity.y += gravity * Time.fixedDeltaTime; // how much the next frame
            }



            //Collision CODE

            Vector2 rayOrigin = new Vector2(position.x + 0.7f, position.y);//In front of Player
            Vector2 rayDirection = Vector2.up;// speed is negative !
            float rayDistance = velocity.y + Time.fixedDeltaTime; // frame distance
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);
            if (hit2D.collider != null)
            {
                Ground ground = hit2D.collider.GetComponent<Ground>();
                if(ground != null )
                {
                    groundHeight = ground.groundHeight; //new groundHeight
                    position.y = groundHeight;
                    velocity.y = 0;
                    isGrounded=true;
                }
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);

        }   
        

        distance += velocity.x * Time.fixedDeltaTime; //distance increases

        if (isGrounded)
        {
            float velocityRatio = velocity.x / maxVelocity;
            acceleration = maxAcceleration + (1 - velocityRatio); // go to zero


            velocity.x += acceleration * Time.fixedDeltaTime;
            
            if(velocity.x >= maxVelocity)
            {
                velocity.x = maxVelocity;
            }


            //Again Collision code

            Vector2 rayOrigin = new Vector2(position.x - 0.7f, position.y);
            Vector2 rayDirection = Vector2.up;// speed is negative !
            float rayDistance = velocity.y + Time.fixedDeltaTime; // frame distance
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);
            if (hit2D.collider == null)
            {
                isGrounded = false;
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.yellow);
        }
        
        transform.position = position;
    }


}
