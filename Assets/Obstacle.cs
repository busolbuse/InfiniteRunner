using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Player player;


    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    
    void Update()
    {
        
    }


    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos.x -= player.velocity.x * Time.fixedDeltaTime;
        if(pos.x < -100)
        {
            Destroy(gameObject);
        }
        transform.position = pos;
    }
}
