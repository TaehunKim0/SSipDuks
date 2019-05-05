using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diagolon : MonoBehaviour
{
    private float deltaX;
    private float deltaY;
    private float elapsedTime;

    void DiagolonMove()
    {
        elapsedTime += Time.deltaTime * 3f;
        deltaY = Mathf.Sin(elapsedTime) * 0.1f;

        Vector2 v = transform.position;
        v.y = deltaY + transform.position.y;
        transform.position = v;

        transform.Translate(-0.2f, 0, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DiagolonMove();
    }
}