using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Vector2 Speed = new Vector2(20, 20);
    private Vector2 Direction = new Vector2(1 , 0);

    private Vector2 MoveMent;
    private Rigidbody2D myRigid;
    private bool UseAngle = false;
    private float Angle = 0f;

    public void SetAngle(float angle)
    {
        Angle = angle;
        UseAngle = true;
    }

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
        if(UseAngle)
        {
            //float width = m_pTarget.position.x - transform.position.x;
           // float height = m_pTarget.position.y - transform.position.y;

            //float radian = Mathf.Atan2(height, width);

            float angle = Angle * 180 / Mathf.PI;

            transform.rotation = Quaternion.Euler(0, 0, angle);

            MoveMent = new Vector2(Mathf.Cos(Angle) * Speed.x, Mathf.Sin(Angle) * Speed.y);
            
        }

        else
        {
            MoveMent = new Vector2(Direction.x * Speed.x, Direction.y * Speed.y);
        }

        
        myRigid.velocity = MoveMent;
        
    }
}