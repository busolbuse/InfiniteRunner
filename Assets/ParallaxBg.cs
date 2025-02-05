using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBg : MonoBehaviour
{
    Player player;

    public float depth = 1; //same depth as the player

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        float realVelocity = player.velocity.x / depth;// the greater the depth, the smaller the speed
        Vector2 position = transform.position;  

        position.x -= realVelocity * Time.fixedDeltaTime; // opposite direction of the player
        
        if(position.x <= -22 )
        {
            position.x = 22;
        }
        
        transform.position = position;

    }
}
