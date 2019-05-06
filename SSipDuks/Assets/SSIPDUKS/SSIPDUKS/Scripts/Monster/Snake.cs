using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private float deltaX;
    private float deltaY;
    private float elapsedTime;

    //Snake
    Vector3 m_pos;
    Vector3 m_offset;

    public void Init(Vector3 pos, Vector3 offset)
    {
        transform.position = pos + offset;
        gameObject.SetActive(true);
        deltaY = 0f;
        elapsedTime = 0f;
    }
    void Update()
    {
        SnakeMove();
    }

    void SnakeMove()
    {
        elapsedTime += Time.deltaTime * 3f;
        deltaY = Mathf.Sin(-elapsedTime) * 0.1f;

        Vector2 v = transform.position;
        v.y = deltaY + transform.position.y;
        transform.position = v;

        transform.Translate(-0.2f, 0, 0);
    }
}
