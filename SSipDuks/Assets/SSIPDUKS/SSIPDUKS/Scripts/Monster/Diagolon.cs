using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diagolon : MonoBehaviour
{
    private float deltaX;
    private float deltaY;
    private float elapsedTime;
    private float time;

    public void Init(Vector3 pos)
    {
        transform.position = pos;
        gameObject.SetActive(true);
    }

    void DiagolonMove()
    {
        if(time >= 3)
        {
            if(deltaY <= -0.03)
            {
                elapsedTime += Time.deltaTime * 3f;
                deltaY = Mathf.Sin(-elapsedTime) * 0.1f;
            }

            Vector2 v = transform.position;
            v.y = deltaY + transform.position.y;
            transform.position = v;

            transform.Translate(-0.2f, 0, 0);
        }
        else
            transform.Translate(-0.2f, 0, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        DiagolonMove();
    }
}