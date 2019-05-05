using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private float deltaX;
    private float deltaY;
    private float elapsedTime;

    float c = 360f;
    void SnakeMove()
    {
        elapsedTime += Time.deltaTime * 9f;
        deltaY = Mathf.Sin(elapsedTime) * 1f;

        Vector2 v = transform.position;
        v.y = deltaY;
        transform.position = v;

        transform.Translate(-0.1f, 0, 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SnakeMove();
    }
}
