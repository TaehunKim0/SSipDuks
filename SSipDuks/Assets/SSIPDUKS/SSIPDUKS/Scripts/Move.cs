using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Vector2 Speed = new Vector2(5, 5);
    public Vector2 Direction = new Vector2(-1, 0);

    public Vector2 MoveMent;
    private Rigidbody2D myRigid;


    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveMent = new Vector2(Speed.x * Direction.x, Speed.y * Direction.y);
    }

    private void FixedUpdate()
    {
        myRigid.velocity = MoveMent;
    }
}
