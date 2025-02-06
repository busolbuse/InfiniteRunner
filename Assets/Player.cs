using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
    public float maxHoldJumpTime = 0.4f; //don't jump so much
    public float max2HoldJumpTime = 0.4f;
    //jump level increasing over time
    public float holdJumpTimer = 0.0f;


    public float jumpGroundThreshold = 1; // close enough to the ground

    //Dead or not Dead :)
    public bool isDead = false;


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


        //DEAD
        if (isDead)
        {
            return;
        }
        if(position.y < -20)
        {
            isDead = true;
        }


        
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



            //Collision on ground

            Vector2 rayOrigin = new Vector2(position.x + 0.7f, position.y);//In front of Player
            Vector2 rayDirection = Vector2.up;// speed is negative !
            float rayDistance = velocity.y + Time.fixedDeltaTime; // frame distance
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);
            if (hit2D.collider != null)
            {
                Ground ground = hit2D.collider.GetComponent<Ground>();
                if(ground != null )
                {
                    if(position.y >= ground.groundHeight) { //player falling down when player hit wall.
                        groundHeight = ground.groundHeight; //new groundHeight
                        position.y = groundHeight;
                        velocity.y = 0;
                        isGrounded = true;
                    }
                    
                }
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);

            //Collision on WALL!!!

            Vector2 wallOrigin = new Vector2(position.x, position.y);
            RaycastHit2D wallHit = Physics2D.Raycast(wallOrigin, Vector2.right, velocity.x * Time.fixedDeltaTime);
            if (wallHit.collider != null)
            {
                Ground ground = wallHit.collider.GetComponent<Ground>();
                if (ground != null)
                {
                    if(position.y < ground.groundHeight)
                    {
                        velocity.x = 0;//forever becomes zero
                    }
                }
            }

        }   
        //collision on block
        
        

        distance += velocity.x * Time.fixedDeltaTime; //distance increases

        if (isGrounded)
        {
            float velocityRatio = velocity.x / maxVelocity;
            acceleration = maxAcceleration * (1 - velocityRatio); // go to zero

            maxHoldJumpTime = max2HoldJumpTime * velocityRatio;

            velocity.x += acceleration * Time.fixedDeltaTime; //jump level increasing over time

            if (velocity.x >= maxVelocity)
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
        //collision on block
        Vector2 obstOrigin = new Vector2(position.x, position.y);
        RaycastHit2D obstHitX = Physics2D.Raycast(obstOrigin, Vector2.right, velocity.x * Time.fixedDeltaTime);
        if(obstHitX.collider != null)
        {
            Obstacle obstacle = obstHitX.collider.GetComponent<Obstacle>();
            if (obstacle !=null)
            {
                hitObstacle(obstacle);
            }
        }

        //for Y
        RaycastHit2D obstHitY = Physics2D.Raycast(obstOrigin, Vector2.up, velocity.y * Time.fixedDeltaTime);
        if (obstHitY.collider != null)
        {
            Obstacle obstacle = obstHitY.collider.GetComponent<Obstacle>();
            if (obstacle != null)
            {
                hitObstacle(obstacle);
            }
        }


        transform.position = position;
    }

    void hitObstacle(Obstacle obstacle)
    {
        Destroy(obstacle.gameObject);
        velocity.x *= 0.7f;
    }

}
