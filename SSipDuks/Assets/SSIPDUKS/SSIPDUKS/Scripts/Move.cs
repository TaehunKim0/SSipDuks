using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Vector2 Speed = new Vector2(5, 5);
    private Vector2 Direction = new Vector2(1 , 0);

    private Vector2 MoveMent;
    private Rigidbody2D myRigid;

    public void SetDirection(Vector2 dir)
    {
        Direction = dir;
    }

    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Direction);
        MoveMent = new Vector2(Direction.x * 20f, Direction.y * 20f);
    }

    private void FixedUpdate()
    {
        myRigid.velocity = MoveMent;
    }
}