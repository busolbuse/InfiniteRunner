using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{

    //Calculate ground height

    public float groundHeight;
    BoxCollider2D collider;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>(); //our own collider
        groundHeight = transform.position.y + (collider.size.y /2);//half

        
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
