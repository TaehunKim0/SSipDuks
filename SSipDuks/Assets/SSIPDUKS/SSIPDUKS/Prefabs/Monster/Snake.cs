using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private float deltaX;
    private float deltaY;
    private float elapsedTime;

    private bool m_CreateSnakes = false;

    //Snake
    Vector3 m_pos;
    int Snakecount = 0;
    Vector3 m_offset;

    public void Init(Vector3 pos, Vector3 offset , bool CreateSnake)
    {
        transform.position = pos + offset;
        m_CreateSnakes = CreateSnake;
        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(m_CreateSnakes)
        {
            StartCoroutine(SpawnSnake(transform.position, m_offset * Snakecount));
            m_CreateSnakes = false;
        }

        SnakeMove();
    }

    void SnakeMove()
    {
        elapsedTime += Time.deltaTime * 3f;
        deltaY = Mathf.Sin(elapsedTime) * 0.1f;

        Vector2 v = transform.position;
        v.y = deltaY + transform.position.y;
        transform.position = v;

        transform.Translate(-0.2f, 0, 0);
    }

    IEnumerator SpawnSnake(Vector3 spos, Vector3 offset)
    {
        while (Snakecount <= 4)
        {
            yield return new WaitForSeconds(0.4f);

            ObjectPool.Instance.PopFromPool("Snake").GetComponent<Snake>().Init(spos, offset * Snakecount, false);

            Snakecount++;
        }
    }
}
