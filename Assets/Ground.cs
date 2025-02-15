using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    Player player;

    //Calculate ground height

    public float groundHeight;
    BoxCollider2D collider;

    //Generation Of grounds
    public float groundRight;
    public float screenRight;

    bool didGenerateGround=false;

    //obstacle
    public Obstacle blockTemplate;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        collider = GetComponent<BoxCollider2D>(); //our own collider
        groundHeight = transform.position.y + (collider.size.y /2);//half
        screenRight= Camera.main.transform.position.x * 2;
        
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos.x -= player.velocity.x * Time.fixedDeltaTime;

        groundRight =transform.position.x + (collider.size.x /2);

        if(groundRight< -20) // grounds that leave the scene, it will be destroy
        {
            Destroy(gameObject);
            return;
        }

        if (!didGenerateGround)
        {
            if (groundRight < screenRight)
            {
                didGenerateGround = true;
                generateGround();
            }
        }
        transform.position= pos;
    }
    void generateGround()
    {
        GameObject go = Instantiate(gameObject); //reference to our game object
        BoxCollider2D goCollider = go.GetComponent<BoxCollider2D>();
        Vector2 pos;

        //max height the Player can jump
        float h1 = player.jumpVelocity * player.maxHoldJumpTime; // hold jump
        float t = player.jumpVelocity / - player.gravity;// calculate time for h2
        float h2 = player.jumpVelocity * t + (0.5f * (player.gravity * (t * t)));// natural jump, we need time
        float maxJumpHeight = h1 + h2;//actual max jump height
        float maxY = transform.position.y + maxJumpHeight;//max actual Y
        maxY *= 0.7f;//buffer
        float minY = 0;

        float actualY = Random.Range(minY, maxY) ; //between minY and maxY


        
        pos.y = actualY - goCollider.size.y / 2;



        //if (pos.y > 6f) //to make sure it doesn't come out of the camera.
        //{
        //    pos.y = 6f;
        //}

        if (pos.y > 6f) //to make sure it doesn't come out of the camera.
        {
            pos.y = 6f;
        }

        float t1 = t + player.maxHoldJumpTime;
        float t2 = Mathf.Sqrt((2.0f * (maxY - actualY)) / -player.gravity);
        float totalTime = t1 + t2;
        float maxX = totalTime * player.velocity.x;
        maxX *= 0.7f;//buffer
        maxX += groundHeight;
        float minX = screenRight + 12;
        float actualX = Random.Range(minX, maxX);

        // pos.x = actualX + goCollider.size.x/2;
        pos.x= minX;
        if(pos.x < minX)
        {
            pos.x = minX;
        }

        go.transform.position = pos;

        Ground goGround = go.GetComponent<Ground>();

        goGround.groundHeight = go.transform.position.y + (goCollider.size.y / 2);//reset ground height

        //test
        //pos.x = screenRight + 12;
        //pos.y = transform.position.y;
        //go.transform.position = pos;

        //ganarate obstacle
        int obstacleNum = Random.Range(0, 3);
        for(int i = 0; i < obstacleNum; i++)
        {
            GameObject block = Instantiate(blockTemplate.gameObject);
            float y = goGround.groundHeight;
            float halfWidth = goCollider.size.x / 2 - 1;//block = 1 br
            float left = go.transform.position.x - halfWidth;
            float right = go.transform.position.x + halfWidth;
            float x = Random.Range(left, right);

            Vector2 blockPos = new Vector2(x,y);
            block.transform.position= blockPos;

        }

    }
}
