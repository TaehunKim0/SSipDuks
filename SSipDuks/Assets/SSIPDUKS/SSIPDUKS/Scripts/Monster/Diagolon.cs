using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diagolon : MonoBehaviour
{
    private float deltaY;
    private float elapsedTime;
    private float time;
    private bool Up;

    public void Init(Vector3 pos)
    {
        transform.position = pos;
        gameObject.SetActive(true);
        elapsedTime = 0f;
        deltaY = 0f;
        time = 0f;

        if (pos.y < 0.3)
        {
            Up = true;
        }
        else
        {
            Up = false;
        }
    }
    
    void DiagolonMove()
    {
        if(time >= 1)
        {
            elapsedTime += 0.02f;
            if(Up)
                deltaY = Mathf.Sin(elapsedTime) * 0.1f;
            else
                deltaY = Mathf.Sin(-elapsedTime) * 0.1f;

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