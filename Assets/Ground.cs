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
        //test
        pos.x = screenRight + 12;
        pos.y = transform.position.y;
        go.transform.position = pos;
        
    }
}
